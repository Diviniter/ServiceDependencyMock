﻿using Mock.Data.Tranfer.Objects.Strategies;

namespace Mock.Define.Strategy.Builder
{
    public class MethodToMock
    {
        public string MethodId;

        public MethodToMockWithMethodStrategy OnceWithMethodMockStrategy(string strategy)
        {
            return new MethodToMockWithMethodStrategy
            {
                MethodId = MethodId,
                MethodMockStrategy = strategy
            };
        }

        public MethodToMockWithObjectStrategy<T> OnceWithObject<T>(T mockedObject)
        {
            return new MethodToMockWithObjectStrategy<T>
            {
                MethodId = MethodId,
                MockedObject = mockedObject
            };
        }

        public ForceNoMockStrategy OnceWithoutMock()
        {
            return new ForceNoMockStrategy
            {
                MethodId = MethodId
            };
        }

        public MethodToMockWithMethodStrategy AlwaysWithMethodMockStrategy(string methodMockStrategy)
        {
            return new MethodToMockWithMethodStrategy
            {
                MethodId = MethodId,
                MethodMockStrategy = methodMockStrategy,
                IsAlwaysApplied = true
            };
        }

        public MethodToMockWithObjectStrategy<T> AlwaysWithObject<T>(T mockedObject)
        {
            return new MethodToMockWithObjectStrategy<T>
            {
                MethodId = MethodId,
                MockedObject = mockedObject,
                IsAlwaysApplied = true
            };
        }
    }
}
