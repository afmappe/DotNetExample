namespace Example.Entities
{
    /// <summary>
    ///
    /// </summary>
    public enum DatabaseTypeCode
    {
        unknown = 0,
        MySql,
        Oracle,
        SqlServer,
    }

    /// <summary>
    /// Objeto que representa los parámetros de conexión a una base de datos
    /// </summary>
    public class DatabaseConfigurationInfo
    {
        public const string MSSQL = "System.Data.SqlClient";

        public const string MYSQL = "MySql.Data.MySqlClient";

        public const string ORACLE = "Oracle.ManagedDataAccess.Client";

        /// <summary>
        ///
        /// </summary>
        public DatabaseConfigurationInfo()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="provider"></param>
        public DatabaseConfigurationInfo(DatabaseTypeCode provider)
        {
            Provider = provider;
        }

        /// <summary>
        ///
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Contraseña de la base de datos
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Proveedor de base de datos
        /// </summary>
        public DatabaseTypeCode Provider
        {
            get
            {
                DatabaseTypeCode result = default(DatabaseTypeCode);

                if (!string.IsNullOrEmpty(ProviderValue))
                {
                    switch (ProviderValue)
                    {
                        case ORACLE: result = DatabaseTypeCode.Oracle; break;
                        case MSSQL: result = DatabaseTypeCode.SqlServer; break;
                        case MYSQL: result = DatabaseTypeCode.MySql; break;
                    }
                }

                return result;
            }
            set
            {
                switch (value)
                {
                    case DatabaseTypeCode.Oracle: ProviderValue = ORACLE; break;
                    case DatabaseTypeCode.MySql: ProviderValue = MYSQL; break;
                    case DatabaseTypeCode.SqlServer: ProviderValue = MSSQL; break;
                }
            }
        }

        /// <summary>
        /// Esquema de la base de datos
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Servidor de base de datos
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Nombre de Usuario de la base de datos
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Cadena de conexión
        /// </summary>
        internal string ConnectionString { get; set; }

        /// <summary>
        /// Proveedor de base de datos
        /// </summary>
        internal string ProviderValue { get; set; }
    }
}