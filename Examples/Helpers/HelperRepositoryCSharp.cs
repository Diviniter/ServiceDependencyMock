﻿using Mock.Dependency.With.Proxy.Apply.Strategy;
using Mock.Dependency.With.Proxy.Data.Transfer.Objects.DatabaseEntities.CSharp;
using Mock.Dependency.With.Proxy.Data.Transfer.Objects.Strategies;
using MockStrategiesCSharp;
using System.Collections.Generic;

namespace IntegrationTests.Helpers
{
    class HelperRepositoryCSharp : HelperRepository
    {
        public IEnumerable<MockStrategy> GetStrategies()
        {
            return MockStrategies.MockStrategy
                        .DeserializeMockStrategies();
        }

        public void RemoveAllStrategies()
        {
            MockStrategies.MockStrategy = new List<MockStrategyEntity>();
        }
    }
}