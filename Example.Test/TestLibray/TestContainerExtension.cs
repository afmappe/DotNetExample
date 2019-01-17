using Example.Infraestructure;
using Example.Interfaces;
using Unity;
using Unity.Lifetime;

namespace TestLibray
{
    public class TestContainerExtension : ContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ITask, TestTask>("TestTask", new PerResolveLifetimeManager());
        }
    }
}