using Example.Entities;
using Example.Infraestructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Lifetime;

namespace Example.Test
{
    [TestClass]
    public class TestBase
    {
        public static DatabaseConfigurationInfo MySqlTestData
        {
            get
            {
                return new DatabaseConfigurationInfo(DatabaseTypeCode.MySql)
                {
                    Database = "",
                    Password = "",
                    Schema = "",
                    Server = "",
                    UserName = ""
                };
            }
        }

        public static DatabaseConfigurationInfo OracleTestData
        {
            get
            {
                return new DatabaseConfigurationInfo(DatabaseTypeCode.Oracle)
                {
                    Password = "",
                    Schema = "",
                    Server = "",
                    UserName = "",
                };
            }
        }

        public static DatabaseConfigurationInfo SqlTestData
        {
            get
            {
                return new DatabaseConfigurationInfo(DatabaseTypeCode.SqlServer)
                {
                    Database = ".",
                    Password = "",
                    Schema = "",
                    Server = "",
                    UserName = ""
                };
            }
        }

        [AssemblyInitialize()]
        public static void Initialize(TestContext context)
        {
            ApplicationContext.Instance.Container.RegisterInstance(DatabaseTypeCode.Oracle.ToString(), OracleTestData, new ContainerControlledLifetimeManager());
            ApplicationContext.Instance.Container.RegisterInstance(DatabaseTypeCode.SqlServer.ToString(), SqlTestData, new ContainerControlledLifetimeManager());
            ApplicationContext.Instance.Container.RegisterInstance(DatabaseTypeCode.MySql.ToString(), MySqlTestData, new ContainerControlledLifetimeManager());
        }
    }
}