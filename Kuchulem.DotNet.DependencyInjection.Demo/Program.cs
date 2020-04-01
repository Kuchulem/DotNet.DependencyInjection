using Kuchulem.DotNet.DependencyInjection.Demo.Services;
using Kuchulem.DotNet.DependencyInjection.Writer;
using System;
using System.Reflection;

namespace Kuchulem.DotNet.DependencyInjection.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            InitServices();

            DependencyInjectionService.Resolve<II18nService>().SetCulture(new System.Globalization.CultureInfo("en"));

            DependencyInjectionService.Resolve<IProgramService>().Run();
        }

        private static void InitServices()
        {
            DependencyInjectionService.Singleton<II18nService, InMemoryI18nService>();
            DependencyInjectionService.Register<IProgramService, NumericalSequenceProgramService>();
            DependencyInjectionService.Register<INumericalSequenceService, FibonacciService>();
            DependencyInjectionService.AddSourceAssembly(Assembly.Load(nameof(Demo.ConsoleWriter)));
            DependencyInjectionService.Register<IWriter>();
        }
    }
}
