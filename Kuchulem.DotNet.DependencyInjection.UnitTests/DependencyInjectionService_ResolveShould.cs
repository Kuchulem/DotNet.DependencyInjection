using Kuchulem.DotNet.DependencyInjection;
using Kuchulem.DotNet.DependencyInjection.Exceptions;
using Kuchulem.DotNet.Tests.DependencyInjection.TestServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection
{
    [TestFixture]
    [Parallelizable]
    [Order(2)]
    class DependencyInjectionService_ResolveShould
    {
        public DependencyInjectionService_ResolveShould()
        {
            DependencyInjectionService.Singleton<ISingletonService, SingletonService>();
            DependencyInjectionService.Register<IWithDependenciesService, WithDependenciesService>();
            DependencyInjectionService.Register<IFooService, FooService>();
            DependencyInjectionService.Register<INotRegisteredDependencyService, NotRegisteredDependencyService>();
            DependencyInjectionService.Register<INotInstanciableService, NotInstanciableService>();
        }

        [Test]
        public void ReturnInterfaceInstance()
        {
            var instance = DependencyInjectionService.Resolve<ILambdaService>();

            Assert.IsInstanceOf<ILambdaService>(instance);
        }

        [Test]
        public void ReturnImplementationInstance()
        {
            var instance = DependencyInjectionService.Resolve<ILambdaService>();

            Assert.IsInstanceOf<LambdaService>(instance);
        }

        [Test]
        public void ReturnDifferentTranscientInstance()
        {
            var instance1 = DependencyInjectionService.Resolve<ILambdaService>();
            var instance2 = DependencyInjectionService.Resolve<ILambdaService>();

            Assert.AreNotEqual(instance1.Guid.ToString(), instance2.Guid.ToString());
        }

        [Test]
        public void ReturnSameSingletonInstance()
        {
            var instance1 = DependencyInjectionService.Resolve<ISingletonService>();
            var instance2 = DependencyInjectionService.Resolve<ISingletonService>();

            Assert.AreEqual(instance1.Guid.ToString(), instance2.Guid.ToString());
        }

        [Test]
        public void ResolveDependencies()
        {
            var instance = DependencyInjectionService.Resolve<IWithDependenciesService>();

            Assert.IsNotNull(instance.FooService);
        }

        [Test]
        public void ThrowNoConstructorFound()
        {
            Assert.Throws<NoSuitableConstructorFoundException>(delegate
            {
                DependencyInjectionService.Resolve<INotRegisteredDependencyService>();
            });
        }

        [Test]
        public void ThrowImplementationNotInstanciableException()
        {
            Assert.Throws<ImplementationNotInstanciableException>(delegate
            {
                DependencyInjectionService.Resolve<INotInstanciableService>();
            });
        }

        [Test]
        public void ThrowNotRegistereDependencyException()
        {
            Assert.Throws<NotRegisteredDependencyException>(delegate
            {
                DependencyInjectionService.Resolve<INotRegisteredService>();
            });
        }
    }
}
