using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    class FibonacciService : INumericalSequenceService
    {
        public List<int> InitialValues { get; }

        public string SequenceName => "Fibonacci Sequence";

        public FibonacciService()
        {
            InitialValues = new List<int> { 0, 1 };
        }

        public int Next()
        {
            var value = InitialValues[InitialValues.Count - 1] + InitialValues[InitialValues.Count - 2];

            InitialValues.Add(value);

            return value;
        }
    }
}
