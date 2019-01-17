using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Interfaces
{
    public interface ITask
    {
        Task RunAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));
    }
}