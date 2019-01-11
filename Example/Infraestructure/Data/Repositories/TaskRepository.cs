using Example.Infraestructure.Data.Context;
using Example.Infraestructure.Data.Entities;
using Example.Tasks.Repositories;
using System.Data.Entity.Infrastructure;

namespace Example.Infraestructure.Data.Repositories
{
    internal class TaskRepository : AsyncRepositoryBase<TaskInfo, TaskContext>, ITaskRepository
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TaskRepository(IDbContextFactory<TaskContext> contextFactory)
            : base(contextFactory)
        { }
    }
}