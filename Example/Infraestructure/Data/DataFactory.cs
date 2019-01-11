using Example.Entities;
using System.Data.Common;

namespace Example.Infraestructure.Data
{
    public static class DataFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static DbConnection GetConnection(DatabaseConfigurationInfo configuration)
        {
            DbConnection result = null;

            DbProviderFactory factory = DbProviderFactories.GetFactory(configuration.ProviderValue);

            result = factory.CreateConnection();
            if (string.IsNullOrEmpty(configuration.ConnectionString))
            {
                result.ConnectionString = GetConnectionString(factory, configuration);
            }
            else
            {
                result.ConnectionString = configuration.ConnectionString;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static string GetConnectionString(DbProviderFactory factory, DatabaseConfigurationInfo configuration)
        {
            string result = null;

            DbConnectionStringBuilder connectionStringBuilder = factory.CreateConnectionStringBuilder();

            if (connectionStringBuilder != null)
            {
                switch (configuration.Provider)
                {
                    case DatabaseTypeCode.SqlServer:
                        connectionStringBuilder.Add("Data Source", configuration.Server);
                        connectionStringBuilder.Add("Initial Catalog", configuration.Database);
                        connectionStringBuilder.Add("User ID", configuration.UserName);
                        connectionStringBuilder.Add("Password", configuration.Password);
                        break;

                    case DatabaseTypeCode.Oracle:
                        connectionStringBuilder.Add("Data Source", configuration.Server);
                        connectionStringBuilder.Add("User ID", configuration.UserName);
                        connectionStringBuilder.Add("Password", configuration.Password);
                        break;

                    case DatabaseTypeCode.MySql:
                        connectionStringBuilder.Add("Server", configuration.Server);
                        connectionStringBuilder.Add("Database", configuration.Database);
                        connectionStringBuilder.Add("User ID", configuration.UserName);
                        connectionStringBuilder.Add("Password", configuration.Password);
                        break;
                }

                result = connectionStringBuilder.ToString();
            }

            return result;
        }
    }
}