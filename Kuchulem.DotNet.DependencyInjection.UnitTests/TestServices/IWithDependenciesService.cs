using System;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    interface IWithDependenciesService
    {
        IFooService FooService { get; }
        Guid Guid { get; }
    }
}