using Example.Infraestructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Test
{
    [TestClass]
    public class UnitTest2
    {
        private readonly MessageQueueWindows QueueModule = new MessageQueueWindows();

        public Task GetFromQueue(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    string a = QueueModule.Receive().ToString();
                    Trace.WriteLine(a);
                }
            }, cancellationToken);
        }

        public void GetFromQueue2()
        {
            MessageQueue queue = new MessageQueue(QueueModule.Path);

            try
            {
                queue.Formatter = new BinaryMessageFormatter();
                queue.ReceiveCompleted += ReceiveCompleted;
                QueueModule.BeginReceive(queue);
                Task.Delay(30000).Wait();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                queue.Dispose();
            }
        }

        [TestMethod]
        public void Queue()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Task.Run(() => SendToQueue(tokenSource.Token)).Wait(1000);
            tokenSource.Cancel();

            GetFromQueue2();

            //tokenSource = new CancellationTokenSource();
            //Task.Run(() => GetFromQueue(tokenSource.Token)).Wait(500);
            //tokenSource.Cancel();

            Trace.WriteLine("End");
        }

        public Task SendToQueue(CancellationToken cancellationToken)
        {
            int i = 0;
            return Task.Run((() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    QueueModule.Send($"Hola Mundo-- {i} --{DateTime.Now}");
                    i++;
                }
            }), cancellationToken);
        }

        private void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            if (sender is MessageQueue queue)
            {
                object obj = e.Message.Body;
                Trace.WriteLine(obj);

                queue.BeginReceive();
            }
        }
    }
}