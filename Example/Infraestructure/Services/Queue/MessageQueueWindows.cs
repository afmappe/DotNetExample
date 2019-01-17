using Example.Util;
using System;
using System.Messaging;

namespace Example.Infraestructure.Services.Queue
{
    internal class MessageQueueWindows : Disposable, IMessageQueue
    {
        private const string defaultPath = @".\Private$\test";

        private MessageQueue _queue;
        private string _queuePath;

        public event EventHandler<ObjectReceivedEventArgs> ObjectReceive;

        public string Path
        {
            get
            {
                return string.IsNullOrEmpty(_queuePath) ? defaultPath : _queuePath;
            }
            set { _queuePath = value; }
        }

        public MessageQueue Queue
        {
            get
            {
                if (_queue == null)
                {
                    _queue = new MessageQueue(Path);

                    _queue.MessageReadPropertyFilter.AppSpecific = true;
                    _queue.Formatter = new BinaryMessageFormatter();
                    _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(ReceiveCompleted);
                }

                return _queue;
            }
        }

        public void BeginReceive()
        {
            Queue.BeginReceive();
        }

        public void Purge()
        {
            using (MessageQueue queue = new MessageQueue(Path))
            {
                queue.Purge();
            }
        }

        public object Receive()
        {
            object result = null;
            using (MessageQueue queue = new MessageQueue(Path))
            {
                queue.Formatter = new BinaryMessageFormatter();
                result = queue.Receive().Body;
            }
            return result;
        }

        public void Send(object obj)
        {
            if (MessageQueue.Exists(Path))
            {
                using (MessageQueue queue = new MessageQueue(Path))
                {
                    queue.Formatter = new BinaryMessageFormatter();
                    queue.Send(obj, DateTime.Now.ToString());
                }
            }
        }

        protected override void InternalDispose()
        {
            if (_queue != null)
            {
                _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(ReceiveCompleted);
                _queue.Close();
                _queue.Dispose();
                _queue = null;
            }
        }

        private void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            if (ObjectReceive != null && sender is MessageQueue queue)
            {
                object current = e.Message.Body;

                {
                    ObjectReceivedEventArgs data = new ObjectReceivedEventArgs { Data = current };
                    ObjectReceive(this, data);
                }
            }
        }
    }
}