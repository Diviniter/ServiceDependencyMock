﻿using ExternalDependency;
using Unity;
using YourApplication.ServiceMethodsStrategies.Get;

namespace YourApplication
{
    public static class IOCContainer
    {
        internal static IUnityContainer container;

        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = new UnityContainer();

                    container.RegisterType<ServiceGetTemplate, ServiceGetOne>(nameof(ServiceGetOne));

                    container.RegisterType<ExternalService, ExternalServiceProxy>();
                }

                return container;
            }
        }
    }
}
