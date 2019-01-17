using Example.Entities;
using Example.Infraestructure;
using Example.Infraestructure.Services.Queue;
using Example.Interfaces;
using Example.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Executor
{
    public class ExecutorModule : Disposable
    {
        private readonly IUnityContainer _Container;

        private CancellationTokenSource TokenSource;

        public ExecutorModule(IUnityContainer Container)
        {
            _Container = Container;
            TokenSource = new CancellationTokenSource();
        }

        private Dictionary<string, IMessageQueue> ListQueues { get; set; }

        public async Task ExecuteTaskAsync(string taskId, Dictionary<string, string> parameters)
        {
            Stopwatch watch = new Stopwatch();

            try
            {
                ITask task = _Container.Resolve<ITask>(taskId);

                using (CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(TokenSource.Token))
                {
                    tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
                    await Task.Run(() => task.RunAsync(parameters, tokenSource.Token));
                }

                watch.Start();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            finally
            {
                watch.Stop();
                Trace.WriteLine($"Execute: {watch.ElapsedMilliseconds} ms");
            }
        }

        public void Initialize(Dictionary<string, string> registers)
        {
            if (registers != null)
            {
                foreach (string key in registers.Keys)
                {
                    RegisterExtension(key, registers[key]);
                }
            }
        }

        public void Start()
        {
            ListQueues = new Dictionary<string, IMessageQueue>();
            string path = @".\Private$\test";
            ListQueues.Add(path, CreateQueue(path));

            Task.Run(() => BeginReceive(), TokenSource.Token);
        }

        public void Stop()
        {
            if (TokenSource != null)
            {
                TokenSource.Cancel();

                if (ListQueues != null)
                {
                    List<string> keys = ListQueues.Keys.ToList();

                    foreach (string topic in keys)
                    {
                        IMessageQueue queue = ListQueues[topic];
                        if (queue != null)
                        {
                            queue.Dispose();
                        }

                        ListQueues.Remove(topic);
                    }
                }
            }
        }

        protected override void InternalDispose()
        {
            TokenSource.Dispose();
            TokenSource = null;
        }

        private void BeginReceive()
        {
            while (ListQueues != null && TokenSource != null && !TokenSource.Token.IsCancellationRequested)
            {
                List<string> keys = ListQueues.Keys.ToList();

                foreach (string topic in keys)
                {
                    IMessageQueue queue = ListQueues[topic];
                    if (queue != null)
                    {
                        queue.BeginReceive();
                    }
                }
            }
        }

        private IMessageQueue CreateQueue(string path)
        {
            IMessageQueue queue = _Container.Resolve<IMessageQueue>("Windows");

            if (queue != null)
            {
                queue.Path = path;
                queue.ObjectReceive += ObjectReceive;
            }

            return queue;
        }

        private Assembly Load(string name)
        {
            Assembly current = Assembly.GetCallingAssembly();
            Uri codeBase = new Uri(current.CodeBase);

            string currentPath = Uri.UnescapeDataString(Path.GetDirectoryName(codeBase.AbsolutePath));
            return Assembly.LoadFrom(Path.Combine(currentPath, name));
        }

        private void ObjectReceive(object sender, ObjectReceivedEventArgs e)
        {
            if (e.Data != null && e.Data is ExecutionInfo ex)
            {
                ExecuteTaskAsync(ex.TaskId, ex.Paramenters);
            }
        }

        private void RegisterExtension(string assemblyName, string className)
        {
            Assembly asm = Load(assemblyName);
            ContainerExtension extensions = (ContainerExtension)asm.CreateInstance(className);
            _Container.AddExtension(extensions);
        }
    }
}