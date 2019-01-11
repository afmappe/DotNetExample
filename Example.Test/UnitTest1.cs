using Example.Entities;
using Example.Infraestructure;
using Example.Tasks.Command;
using Example.Tasks.Queries;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Unity;

namespace Example.Test
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            IMediator mediator = ApplicationContext.Instance.Container.Resolve<IMediator>();

            //ApplicationContext.Instance.Database = DatabaseTypeCode.SqlServer;
            //CreateTask(mediator);

            //ApplicationContext.Instance.Database = DatabaseTypeCode.Oracle;
            //CreateTask(mediator);

            ApplicationContext.Instance.Database = DatabaseTypeCode.MySql;
            CreateTask(mediator);
        }

        private static void CreateTask(IMediator mediator)
        {
            int id = Task.Run(() => mediator.Send(new CreateTaskCommand.Request { Description = "a", Name = "a" })).Result;
            Assert.IsTrue(id != 0);

            var t = mediator.Send(new GetTaskQuery.Request { Id = id }).Result;
        }
    }
}