using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    abstract class NotInstanciableService : LambdaService, INotInstanciableService
    {
    }
}
