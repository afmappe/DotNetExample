using Example.Extensions;
using Example.Infraestructure.Data;
using Example.Infraestructure.Data.Context;
using Example.Infraestructure.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace Example.Infraestructure
{
    public class ContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Trace.WriteLine("Register Extension");

            Container
                .RegisterType<IDbContextFactory<TaskContext>, DbContextFactory<TaskContext>>(new ContainerControlledLifetimeManager())
                .RegisterGenericInterface(typeof(IDbContextFactory<>), () => new ContainerControlledLifetimeManager())
                .RegisterGenericInterface(typeof(IAsyncRepository<>), () => new PerResolveLifetimeManager())
                .RegisterInterface(typeof(IQueryRepository), () => new PerResolveLifetimeManager())
                .RegisterMediator(new PerResolveLifetimeManager());
        }
    }
}