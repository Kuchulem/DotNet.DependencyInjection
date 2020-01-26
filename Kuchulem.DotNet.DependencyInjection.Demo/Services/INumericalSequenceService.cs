using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    interface INumericalSequenceService
    {
        string SequenceName { get; }

        List<int> InitialValues { get; }

        int Next();
    }
}
