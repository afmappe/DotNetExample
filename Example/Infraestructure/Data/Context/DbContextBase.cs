using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Example.Infraestructure.Data.Context
{
    public class DbContextBase : DbContext, IDbModelCacheKeyProvider
    {
        protected readonly string Key;
        protected readonly string Schema;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Una conexión existente para usar para el nuevo contexto.</param>
        /// <param name="schema">Esquema de la base de datos</param>
        public DbContextBase(DbConnection connection, string schema) :
            base(connection, true)
        {
            Schema = schema;
            Key = string.Format("{0}.{1}", GetType().FullName, Schema);

            Configuration.ProxyCreationEnabled = false;
        }

        public string CacheKey { get { return Key; } }
    }
}