using Kuchulem.DotNet.DependencyInjection.Writer;
using System;

namespace Kuchulem.DotNet.DependencyInjection.Demo.ConsoleWriter
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(object data)
        {
            Console.WriteLine(data);
        }
    }
}
