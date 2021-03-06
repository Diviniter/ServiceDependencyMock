﻿using Mock.Dependency.With.Proxy.Data.Transfer.Objects.Strategies;
using NFluent;
using Optional;
using Xunit;

namespace Mock.Dependency.With.Proxy.Define.Strategy.Tests
{
    public class MockBuilderTest
    {
        [Fact]
        public void Should_build_noMockStrategy_with_context()
        {
            //Arrange
            var methodId = "id";
            var context = new MockContext { };

            //Act
            var mockStrategy = MockStrategyBuilder
                .ForMethod(methodId)
                .OnceWithoutMock()
                .WithContext(context);

            //Assert
            var expectedMockStrategy = new ForceNoMockStrategy()
            {
                MethodId = methodId,
                Context = Option.Some<dynamic>(context)
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_noMockStrategy()
        {
            //Arrange
            var methodId = "id";

            //Act
            var mockStrategy = MockStrategyBuilder
                .ForMethod(methodId)
                .OnceWithoutMock();

            //Assert
            var expectedMockStrategy = new ForceNoMockStrategy()
            {
                MethodId = methodId
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_mockMethodStrategy_with_context()
        {
            //Arrange
            var methodId = "id";
            var context = new MockContext { };

            //Act
            var mockStrategy = MockStrategyBuilder
                .ForMethod(methodId)
                .OnceWithSubstituteBehavior(nameof(MockMethodStrategy))
                .WithContext(context);

            //Assert
            var expectedMockStrategy = new SubstituteBehaviorStrategy()
            {
                MethodId = methodId,
                BehaviorName = nameof(MockMethodStrategy),
                Context = Option.Some<dynamic>(context)
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_callOnceMockMethodStrategy()
        {
            //Arrange
            var methodId = "id";

            //Act
            var mockStrategy = MockStrategyBuilder.ForMethod(methodId)
                .OnceWithSubstituteBehavior(nameof(MockMethodStrategy));

            //Assert
            var expectedMockStrategy = new SubstituteBehaviorStrategy()
            {
                MethodId = methodId,
                BehaviorName = nameof(MockMethodStrategy)
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_callAlwaysMockMethodStrategy()
        {
            //Arrange
            var methodId = "id";

            //Act
            var mockStrategy = MockStrategyBuilder.ForMethod(methodId)
                .AlwaysWithSubstituteBehavior(nameof(MockMethodStrategy));

            //Assert
            var expectedMockStrategy = new SubstituteBehaviorStrategy()
            {
                MethodId = methodId,
                BehaviorName = nameof(MockMethodStrategy),
                IsAlwaysApplied = true
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_mockObjectStrategy_with_context()
        {
            //Arrange
            var methodId = "id";
            var mockedObject = 1;
            var context = new MockContext { };

            //Act
            var mockStrategy = MockStrategyBuilder.ForMethod(methodId)
                .OnceWithObject(mockedObject)
                .WithContext(context);

            //Assert
            var expectedMockStrategy = new ObjectStrategy<int>()
            {
                MethodId = methodId,
                MockedObject = mockedObject,
                Context = Option.Some<dynamic>(context)
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_callAlwaysMockObjectStrategy()
        {
            //Arrange
            var methodId = "id";
            var mockedObject = 1;

            //Act
            var mockStrategy = MockStrategyBuilder.ForMethod(methodId)
                .AlwaysWithObject(mockedObject);

            //Assert
            var expectedMockStrategy = new ObjectStrategy<int>
            {
                MethodId = methodId,
                MockedObject = mockedObject,
                IsAlwaysApplied = true
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }

        [Fact]
        public void Should_build_callOnceMockObjectStrategy()
        {
            //Arrange
            var methodId = "id";
            var mockedObject = 1;

            //Act
            var mockStrategy = MockStrategyBuilder.ForMethod(methodId)
                .OnceWithObject(mockedObject);

            //Assert
            var expectedMockStrategy = new ObjectStrategy<int>
            {
                MethodId = methodId,
                MockedObject = mockedObject
            };
            Check.That(mockStrategy).IsEqualTo(expectedMockStrategy);
        }
    }
}
