using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    public interface IGenericService<T>
    {
        Guid Guid { get; }

        T GenericData { get; set; }
    }
}
