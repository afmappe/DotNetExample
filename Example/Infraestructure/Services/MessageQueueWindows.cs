using System.Messaging;

namespace Example.Infraestructure.Services
{
    public class MessageQueueWindows
    {
        public string Path
        {
            get
            {
                return @".\Private$\test";
            }
        }

        public void BeginReceive(MessageQueue queue)
        {
            queue.BeginReceive();
        }

        public void Clear()
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
                    queue.Send(obj, obj.ToString());
                }
            }
        }
    }
}