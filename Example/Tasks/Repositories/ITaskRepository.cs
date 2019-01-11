using Example.Infraestructure.Data.Entities;
using Example.Infraestructure.Interfaces;

namespace Example.Tasks.Repositories
{
    public interface ITaskRepository : IAsyncRepository<TaskInfo>
    {
    }
}