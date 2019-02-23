﻿using ExternalDependency;
using Mock.ApplicationSide;
using Mock.ApplicationSide.ServiceMethodsStrategies.Get;
using Mock.ClientSide;
using Mock.Library;
using Mock.Library.ApplicationSide;
using Mock.Library.ClientSide;
using NFluent;
using NSubstitute;
using SharedDatabase;
using System;
using System.Collections.Generic;
using Xunit;
using static Mock.ApplicationSide.ServiceMethodsStrategies.ServiceMethodsIdentifiers;

namespace Tests
{
    public class ServiceProxyTest : IDisposable
    {
        private readonly ServiceProxy serviceProxy;

        private readonly ClientSideRepository clientSideRepository;
        private readonly ApplicationSideQuery serviceSideQuery;
        private readonly Service service;

        public ServiceProxyTest()
        {
            this.clientSideRepository = new ClientSideRepositoryImpl();
            this.serviceSideQuery = new ApplicationSideQueryImpl();
            this.service = Substitute.For<Service>();
            this.serviceProxy = new ServiceProxy(this.serviceSideQuery, this.service);
        }

        public void Dispose()
        {
            MockStrategiesDatabase.MockStrategies = new List<MockStrategy>();
            ApplicationDatabase.SessionId = null;
        }

        [Fact]
        public void Should_mock_method_behavior()
        {
            //Arrange
            var mockMethodStrategy = MockStrategyBuilder.ForMethod(Get).WithStrategy(nameof(ServiceGetOne));
            this.clientSideRepository.MockMethod(mockMethodStrategy);

            //Act
            var result = this.serviceProxy.Get();

            //Assert
            Check.That(result).IsEqualTo(1);
        }

        [Fact]
        public void Should_not_mock_When_no_strategy_defined()
        {
            //Arrange
            this.service.Get().Returns(0);

            //Act
            this.serviceProxy.Get();

            //Assert
            this.service.Received().Get();
        }

        [Fact]
        public void Should_not_mock_When_strategy_concern_another_method()
        {
            //Arrange
            var methodMockStrategy = MockStrategyBuilder.ForMethod("fakeMethodId")
                                            .WithStrategy(nameof(ServiceGetOne));
            this.clientSideRepository.MockMethod(methodMockStrategy);

            //Act
            this.serviceProxy.Get();

            //Assert
            this.service.Received().Get();
        }

        [Fact]
        public void Should_crash_When_unexisting_method_strategy_is_used()
        {
            //Arrange
            var methodMockStrategy = MockStrategyBuilder.ForMethod(Get).WithStrategy("unexisting strategy");
            this.clientSideRepository.MockMethod(methodMockStrategy);

            //Act
            Action action = () => this.serviceProxy.Get();

            //Assert
            Check.ThatCode(action).Throws<Exception>()
                 .WithMessage($"Method strategy '{methodMockStrategy.Strategy}' is not defined");
        }

        [Fact]
        public void Should_mock_with_object_When_defined()
        {
            //Arrange
            var specificObject = 10;
            var mockObjectStrategy = MockStrategyBuilder.ForMethod(Get).WithObject(specificObject);
            this.clientSideRepository.MockObject(mockObjectStrategy);

            //Act
            var result = this.serviceProxy.Get();

            //Assert
            Check.That(result).IsEqualTo(specificObject);
        }

        [Fact]
        public void Should_mock_with_First_In_First_Out_Strategy()
        {
            //Arrange
            var firstMockStrategy = MockStrategyBuilder.ForMethod(Get).WithObject(1);
            var secondMockStrategy = MockStrategyBuilder.ForMethod(Get).WithObject(2);

            this.clientSideRepository.MockObject(firstMockStrategy);
            this.clientSideRepository.MockObject(secondMockStrategy);

            //Act
            var result1 = this.serviceProxy.Get();
            var result2 = this.serviceProxy.Get();

            //Assert
            Check.That(result1).IsEqualTo(1);
            Check.That(result2).IsEqualTo(2);
        }

        [Fact]
        public void Should_not_mock_one_call_between_two_mocked_call()
        {
            //Arrange
            var firstMockStrategy = MockStrategyBuilder.ForMethod(Get).WithObject(1);
            var secondMockStrategy = MockStrategyBuilder.ForMethod(Get).WithoutMock();
            var thirdMockStrategy = MockStrategyBuilder.ForMethod(Get).WithObject(3);

            this.service.Get().Returns(2);

            this.clientSideRepository.MockObject(firstMockStrategy);
            this.clientSideRepository.DontMock(secondMockStrategy);
            this.clientSideRepository.MockObject(thirdMockStrategy);

            //Act
            var result1 = this.serviceProxy.Get();
            var result2 = this.serviceProxy.Get();
            var result3 = this.serviceProxy.Get();

            //Assert
            Check.That(result1).IsEqualTo(firstMockStrategy.MockedObject);
            Check.That(result2).IsEqualTo(2);
            Check.That(result3).IsEqualTo(thirdMockStrategy.MockedObject);
        }

        [Fact]
        public void Should_mock_with_object_When_context_is_the_same()
        {
            //Arrange
            var sessionId = Guid.NewGuid().ToString();

            ApplicationDatabase.SessionId = sessionId;

            var mockStrategyWithSameContext = MockStrategyBuilder.ForMethod(Get)
                .WithObject(1)
                .WithContext(new GetMockContext { SessionId = sessionId });

            this.clientSideRepository.MockObject(mockStrategyWithSameContext);

            //Act
            var result = this.serviceProxy.Get();

            //Assert
            Check.That(result).IsEqualTo(mockStrategyWithSameContext.MockedObject);
        }

        [Fact]
        public void Should_not_mock_When_context_is_not_the_same()
        {
            //Arrange
            ApplicationDatabase.SessionId = Guid.NewGuid().ToString();

            var mockStrategyWithDifferentContext = MockStrategyBuilder.ForMethod(Get)
                .WithObject(1)
                .WithContext(new GetMockContext { SessionId = Guid.NewGuid().ToString() });
            this.clientSideRepository.MockObject(mockStrategyWithDifferentContext);

            this.service.Get().Returns(2);

            //Act
            var result = this.serviceProxy.Get();

            //Assert
            Check.That(result).IsEqualTo(2);
        }
    }
}
