using System.Threading.Tasks;

namespace Example.Infraestructure.Interfaces
{
    public interface IAsyncRepository<in TEntityType>
        where TEntityType : class
    {
        /// <summary>
        /// Crea una entidad en el contexto
        /// </summary>
        /// <param name="record">Entidad a crear</param>
        /// <returns>Entidad creado</returns>
        Task Create(TEntityType record);

        /// <summary>
        /// Elimina una entidad del contexto
        /// </summary>
        /// <param name="record">Entidad a eliminar (no es necesario obtenerla previamente
        /// basta con establecer las propiedades que correspondan a las llaves primarias)</param>
        Task Delete(TEntityType record);
    }
}