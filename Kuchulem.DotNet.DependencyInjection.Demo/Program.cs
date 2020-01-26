using Kuchulem.DotNet.DependencyInjection.Demo.Services;
using System;

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
        }
    }
}
