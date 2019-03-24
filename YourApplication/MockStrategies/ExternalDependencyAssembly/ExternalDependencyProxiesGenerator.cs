﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a T4 template.
//     Generated on: 24/03/2019 17:53:32
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated. Re-run the T4 template to update this file.
// </auto-generated>
//------------------------------------------------------------------------------
using Mock.Dependency.With.Proxy.Apply.Strategy;
using Mock.Dependency.With.Proxy.Data.Transfer.Objects.Strategies;
using System;
using StructureMap;
using System.Reflection;
using StructureMapConfigurationException = StructureMap.StructureMapConfigurationException;

using ExternalDependency;
using static CentralizedInformations.ExtenalServiceMethodsIds;

namespace Mock.Dependency.With.Proxy.Apply.Strategy
{
	
	public interface ExternalServiceBrokenMethodTemplate
	{
		int BrokenMethod();
	}

	public partial class ExternalServiceProxy : ProxyBase, ExternalService
    {
        private readonly ExternalService service;
        private readonly MockStrategyRepository mockStrategyQuery;

        public ExternalServiceProxy(MockStrategyRepository mockStrategyQuery, ExternalService service)
        {
            this.mockStrategyQuery = mockStrategyQuery;
            this.service = service;
        }

		public  int BrokenMethod()
        {
           int returnedValue;

            var mockStrategy = this.mockStrategyQuery.GetMockStrategy(BrokenMethodId, this.InWantedContext());

            if (NoMockStrategy(mockStrategy))
            {
                returnedValue = this.service.BrokenMethod();
            }
            else if (mockStrategy is ObjectStrategy<int> objectStrategy)
            {
                returnedValue = objectStrategy.MockedObject;
            }
            else if (mockStrategy is SubstituteBehaviorStrategy methodStrategy)
            {
                returnedValue = ApplyMethodMockStrategyBrokenMethod(methodStrategy  );
            }
            else
            {
                throw new Exception("Current mock strategy is not take in account");
            }

            this.mockStrategyQuery.RemoveStrategy(mockStrategy);

            return returnedValue;
        }
		
		private static int ApplyMethodMockStrategyBrokenMethod(SubstituteBehaviorStrategy substituteBehaviorStrategy  )
        {
			try
            {
                var serviceSubstitute = ProxyContainer.Container.GetInstance<ExternalServiceBrokenMethodTemplate>(substituteBehaviorStrategy.BehaviorName);
				return serviceSubstitute.BrokenMethod();
            }
            catch (StructureMapConfigurationException)
            {
                throw new Exception($"Method strategy '{substituteBehaviorStrategy.BehaviorName}' is not defined");
            }            
        }
		
		private static bool NoMockStrategy(MockStrategy mockStrategy)
        {
            return mockStrategy is NoMockStrategy || mockStrategy is ForceNoMockStrategy;
        }
	}
	
	public static class ProxyContainer
    {
        private static Container container;
	
        public static Container Container
        {
            get
            {
                if (container == null)
                {
                    container = new Container(c =>
                    {
                        c.Scan(_ =>
                        {
                            _.Assembly(Assembly.GetExecutingAssembly());
                            _.AddAllTypesOf<ExternalServiceBrokenMethodTemplate>().NameBy(x => x.Name);
                        });
                    });
                }
	
                return container;
            }
        }
    }

}
    

