using System;
using System.Diagnostics;
using System.Threading;

namespace Executor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            Mutex mutex = null;
            try
            {
                ExecuterService service = new ExecuterService();

                mutex = new Mutex(true, "Global\\Executor", out bool createdNew);

                if (createdNew)
                {
#if (!DEBUG)
                    System.ServiceProcess.ServiceBase[] ServicesToRun;
                    ServicesToRun = new System.ServiceProcess.ServiceBase[]
                    {
                        service
                    };
                    ServiceBase.Run(ServicesToRun);
#else
                    service.InternalStart();
                    Thread.Sleep(Timeout.Infinite);

#endif
                }
                else
                {
                    Trace.WriteLine("Already Running");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                    mutex.Dispose();
                }
            }
        }
    }
}