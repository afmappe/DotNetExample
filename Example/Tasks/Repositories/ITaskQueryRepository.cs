using Example.Infraestructure.Data.Entities;
using Example.Infraestructure.Interfaces;
using System.Threading.Tasks;

namespace Example.Tasks.Repositories
{
    public interface ITaskQueryRepository : IQueryRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TaskInfo> Get(int id);
    }
}