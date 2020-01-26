using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    class NotRegisteredDependencyService : INotRegisteredDependencyService
    {
        public NotRegisteredDependencyService(INotRegisteredService notRegisteredService)
        {
            NotRegisteredService = notRegisteredService;
        }

        public INotRegisteredService NotRegisteredService { get; }
    }
}
