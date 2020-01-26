using System;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    class WithDependenciesService : IWithDependenciesService
    {
        public WithDependenciesService(IFooService fooService)
        {
            FooService = fooService;
        }
        public Guid Guid { get; private set; }
        public IFooService FooService { get; }
    }
}
