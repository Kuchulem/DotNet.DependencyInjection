﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.Tests.DependencyInjection.TestServices
{
    interface INotRegisteredDependencyService
    {
        INotRegisteredService NotRegisteredService { get; }
    }
}
