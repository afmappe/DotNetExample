using Example.Entities;
using Example.Infraestructure.Data;
using System.Data.Entity.Infrastructure.Interception;
using Unity;

namespace Example.Infraestructure
{
    public class ApplicationContext : ApplicationInstance
    {
        #region Singleton Pattern

        /// <summary>
        /// Objeto de bloqueo para creación de instancia
        /// </summary>
        private static readonly object sync = new object();

        /// <summary>
        /// Objeto para la creación de instancia
        /// </summary>
        private static volatile ApplicationContext instance;

        /// <summary>
        /// Referencia a la única instancia
        /// </summary>
        public static ApplicationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new ApplicationContext();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion
    }

    public class ApplicationInstance
    {
        /// <summary>
        /// Contenedor de dependencias
        /// </summary>
        public readonly IUnityContainer Container;

        public ApplicationInstance()
        {
            Container = new UnityContainer();
            Container.AddNewExtension<ContainerExtension>();

            //Adicionar interceptor UTC
            DbInterception.Add(new UtcDbCommandInterceptor());
        }

        /// <summary>
        /// Tipo de base de datos
        /// </summary>
        public DatabaseTypeCode Database { get; set; }
    }
}