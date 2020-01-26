using Kuchulem.DotNet.DependencyInjection;
using Kuchulem.DotNet.DependencyInjection.Exceptions;
using Kuchulem.DotNet.Tests.DependencyInjection.TestServices;
using NUnit.Framework;

namespace Kuchulem.DotNet.Tests.DependencyInjection
{
    [TestFixture]
    [Parallelizable]
    [Order(1)]
    public class DependencyInjectionService_RegisterShould
    {
        public DependencyInjectionService_RegisterShould()
        {
            DependencyInjectionService.Register<ILambdaService, LambdaService>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ThrowAlreadyRegisteredDependencyException()
        {
            Assert.Throws<AlreadyRegisteredDependencyException>(delegate { DependencyInjectionService.Register<ILambdaService, LambdaService>(); });
        }

        [Test]
        public void NotThrowAlreadyRegisteredDependencyException()
        {
            Assert.DoesNotThrow(delegate
            {
                DependencyInjectionService.Register<IGenericService<long>, GenericService<long>>();
                DependencyInjectionService.Register<IGenericService<int>, GenericService<int>>();
            });
        }
    }
}