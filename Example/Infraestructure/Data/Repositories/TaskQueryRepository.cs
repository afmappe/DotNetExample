using Example.Infraestructure.Data.Context;
using Example.Infraestructure.Data.Entities;
using Example.Tasks.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Infraestructure.Data.Repositories
{
    internal class TaskQueryRepository : QueryRepository<TaskContext>,
        ITaskQueryRepository
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="contextFactory"></param>
        public TaskQueryRepository(IDbContextFactory<TaskContext> contextFactory)
            : base(contextFactory)
        { }

        public async Task<TaskInfo> Get(int id)
        {
            TaskInfo result = null;
            try
            {
                using (TaskContext context = CreateContext())
                {
                    var query = context.Task.Where(x => x.Id == id);
                    result = await query.FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Data", ex);
            }

            return result;
        }
    }
}