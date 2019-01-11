using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Lifetime;

namespace Example.Extensions
{
    public static class IUnityContainerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="container"></param>
        /// <param name="genericInterfaceType"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        public static IUnityContainer RegisterGenericInterface(
            this IUnityContainer container,
            Type genericInterfaceType,
            Func<LifetimeManager> lifetimeManager)
        {
            RegisterTypes(
                container,
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t =>
                        t.IsClass &&
                        !t.IsAbstract &&
                        t.GetInterfaces()
                            .Any(i => IsSubclassOfRawGeneric(genericInterfaceType, i))),
                lifetimeManager);

            return container;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="container"></param>
        /// <param name="interfaceType"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        public static IUnityContainer RegisterInterface(
            this IUnityContainer container,
            Type interfaceType,
            Func<LifetimeManager> lifetimeManager)
        {
            RegisterTypes(
                container,
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x =>
                        x.IsClass &&
                        !x.IsAbstract &&
                        x.GetInterfaces().Any(i => i == interfaceType)),
                lifetimeManager);

            return container;
        }

        public static IUnityContainer RegisterMediator(this IUnityContainer container, LifetimeManager lifetimeManager)
        {
            container
                .RegisterType<IMediator, Mediator>(lifetimeManager)
                .RegisterGenericInterface(typeof(IRequestHandler<,>), () => new PerResolveLifetimeManager())
                .RegisterGenericInterface(typeof(INotificationHandler<>), () => new PerResolveLifetimeManager())
                .RegisterInstance<ServiceFactory>(type =>
                {
                    var enumerableType = type
                        .GetInterfaces()
                        .Concat(new[] { type })
                        .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                    return enumerableType != null
                        ? container.ResolveAll(enumerableType.GetGenericArguments()[0])
                        : container.IsRegistered(type)
                            ? container.Resolve(type)
                            : null;
                });

            return container;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="generic"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var currentType = toCheck.IsGenericType ?
                toCheck.GetGenericTypeDefinition() :
                toCheck;

                if (generic == currentType)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="container"></param>
        /// <param name="types"></param>
        /// <param name="lifetimeManager"></param>
        private static void RegisterTypes(IUnityContainer container, IEnumerable<Type> types, Func<LifetimeManager> lifetimeManager)
        {
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    container.RegisterType(@interface, type, lifetimeManager());
                }
            }
        }
    }
}