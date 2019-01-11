using Example.Entities;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Unity;

namespace Example.Infraestructure.Data
{
    /// <summary>
    /// Fabrica para la creación de contextos
    /// </summary>
    /// <typeparam name="TContextType"></typeparam>
    public class DbContextFactory<TContextType> : IDbContextFactory<TContextType>
        where TContextType : DbContext
    {
        public TContextType Create()
        {
            TContextType context = null;

            string provider = ApplicationContext.Instance.Database.ToString();
            DatabaseConfigurationInfo configuration = ApplicationContext.Instance.Container.Resolve<DatabaseConfigurationInfo>(provider);

            DbConnection connection = DataFactory.GetConnection(configuration);

            if (connection != null)
            {
                context = (TContextType)Activator.CreateInstance(typeof(TContextType), connection, configuration.Schema);
            }

            return context;
        }
    }
}