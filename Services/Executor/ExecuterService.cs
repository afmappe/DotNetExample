using Example.Infraestructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Timer = System.Timers.Timer;

namespace Executor
{
    public partial class ExecuterService : ServiceBase
    {
        private ExecutorModule _Module;
        private Timer _Timer;
        private CancellationTokenSource _TokenSource;

        public ExecuterService()
        {
            InitializeComponent();
            _TokenSource = new CancellationTokenSource();
        }

        ~ExecuterService()
        {
            _TokenSource.Dispose();
            _TokenSource = null;
        }

        public ExecutorModule Module
        {
            get
            {
                if (_Module == null)
                {
                    _Module = new ExecutorModule(ApplicationContext.Instance.Container.CreateChildContainer());
                }
                return _Module;
            }
        }

        private Timer Timer
        {
            get
            {
                if (_Timer == null)
                {
                    _Timer = new Timer { AutoReset = false, };
                    _Timer.Elapsed += OnStartTimer;
                }
                return _Timer;
            }
        }

        internal void InternalStart()
        {
            Timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            Timer.Start();
        }

        protected override void OnStart(string[] args)
        {
            InternalStart();
        }

        protected override void OnStop()
        {
            Trace.WriteLine("Stop Service");
            _TokenSource.Cancel();
        }

        private void OnStartTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Trace.WriteLine("Service Starting");

                Module.Initialize(new Dictionary<string, string> { { "TestLibray.dll", "TestLibray.TestContainerExtension" } });

                Trace.WriteLine("Service Initialize");

                Module.Start();

                Trace.WriteLine("Service Started");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }
    }
}