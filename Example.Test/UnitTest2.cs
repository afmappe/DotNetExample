using Example.Entities;
using Example.Infraestructure;
using Example.Infraestructure.Services.Queue;
using Executor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Example.Test
{
    [TestClass]
    public class UnitTest2
    {
        private readonly IUnityContainer _Container;

        private StringBuilder builder;

        public UnitTest2()
        {
            _Container = ApplicationContext.Instance.Container.CreateChildContainer();

            builder = new StringBuilder();
        }

        ~UnitTest2()
        {
            _Container.Dispose();
            ApplicationContext.Instance.Container.Dispose();
        }

        [TestMethod]
        public void Executor()
        {
            ExecutorModule module = new ExecutorModule(_Container);
            module.Initialize(new Dictionary<string, string> { { "TestLibray.dll", "TestLibray.TestContainerExtension" } });

            IMessageQueue queue = _Container.Resolve<IMessageQueue>("Windows");
            queue.Purge();

            queue.Send(new ExecutionInfo { TaskId = "TestTask", Paramenters = new Dictionary<string, string> { { "name", "Task1" } } });
            queue.Send(new ExecutionInfo { TaskId = "TestTask", Paramenters = new Dictionary<string, string> { { "name", "Task2" } } });

            Task.Run(() => module.Start());
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();

            module.Stop();
            module.Dispose();

            Trace.WriteLine("END");
        }

        [TestMethod]
        public void Queue()
        {
            using (IMessageQueue queue = _Container.Resolve<IMessageQueue>("Windows"))
            {
                queue.ObjectReceive += ObjectReceive;

                queue.Purge();

                AddToQueue(queue);
                ReceiveFromQueue(queue);
            }
            Trace.WriteLine(builder.ToString());
            Trace.WriteLine("END");
        }

        private void AddToQueue(IMessageQueue queue)
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                Task task = Task.Run(() =>
                {
                    int i = 0;
                    while (!tokenSource.Token.IsCancellationRequested)
                    {
                        queue.Send($"{i}");
                        i++;
                    }
                }, tokenSource.Token);
                tokenSource.CancelAfter(TimeSpan.FromSeconds(2));
                Task.WaitAll(task);
            }
        }

        private void ObjectReceive(object sender, ObjectReceivedEventArgs e)
        {
            builder.Append($"{e.Data.ToString()},");
        }

        private void ReceiveFromQueue(IMessageQueue queue)
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                Task task = Task.Run(() =>
                {
                    while (!tokenSource.Token.IsCancellationRequested)
                    {
                        queue.BeginReceive();
                    }
                }, tokenSource.Token);
                tokenSource.CancelAfter(TimeSpan.FromSeconds(1));
                Task.WaitAll(task);
            }
        }
    }
}