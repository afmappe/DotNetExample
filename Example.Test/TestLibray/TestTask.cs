using Example.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TestLibray
{
    public class TestTask : ITask
    {
        public async Task RunAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            int i = 0;

            Trace.WriteLine($"Task Start {DateTime.Now}");

            await Task.Run(() =>
             {
                 while (!cancellationToken.IsCancellationRequested)
                 {
                     Trace.WriteLine($"Task {parameters["name"]} Running {DateTime.Now} Number ${i}");

                     i++;
                 }
             }, cancellationToken);

            Trace.WriteLineIf(!cancellationToken.IsCancellationRequested, $"Task End {DateTime.Now}");
            Trace.WriteLineIf(cancellationToken.IsCancellationRequested, $"Task Cancel {parameters["name"]} -- {DateTime.Now}");
        }
    }
}