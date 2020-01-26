using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    public class LambdaService : ILambdaService
    {
        public Guid Guid { get; private set; }

        public LambdaService()
        {
            Guid = Guid.NewGuid();
        }
    }
}
