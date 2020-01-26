using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    public class GenericService<T> : IGenericService<T>
    {
        public Guid Guid { get; private set; }

        public T GenericData { get; set; }

        public GenericService()
        {
            Guid = Guid.NewGuid();
        }
    }
}
