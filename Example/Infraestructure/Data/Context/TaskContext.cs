using Example.Infraestructure.Data.Entities;
using Example.Infraestructure.Data.Mapper;
using System.Data.Common;
using System.Data.Entity;

namespace Example.Infraestructure.Data.Context
{
    public class TaskContext : DbContextBase
    {
        /// <summary>
        /// Setting the initializer to null will skip the model compatibility check.
        /// </summary>
        static TaskContext()
        {
            Database.SetInitializer<TaskContext>(null);
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="connection">Una conexión existente para usar para el nuevo contexto.</param>
        /// <param name="schema">Esquema de la base de datos</param>
        public TaskContext(DbConnection connection, string schema)
            : base(connection, schema)
        { }

        /// <summary>
        ///
        /// </summary>
        public DbSet<TaskInfo> Task { get; set; }

        /// <summary>
        /// <see cref="DbContext.OnModelCreating(DbModelBuilder)"/>
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TaskMapper(Schema));
        }
    }
}