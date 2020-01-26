using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    abstract class BaseProgramService : IProgramService
    {
        protected II18nService i18nService;
        private readonly IProgramService programService;

        public void Run()
        {
            bool keepRunning = true;

            while(keepRunning)
            {
                Display("Welcome");

                var culture = Question("Language ?", new[] { "en", "fr" }, (string answer) => new CultureInfo(answer));

                i18nService.SetCulture(culture);

                DoRun();

                keepRunning = Question("Continue ?", new[] { "y", "yes", "n", "no"}, (string answer) => answer == "y" || answer == "yes");
            }

            Display("Goodbye");
        }

        protected void Display(string token, object?[] args = null)
        {
            if(args is null)
            {
                Console.WriteLine(i18nService.Translate(token));
                return;
            }

            for (var i = 0; i < args.Length; i++)
                args[i] = (args[i] is string) ? i18nService.Translate((string)args[i]) : args[i];

            Console.WriteLine(string.Format(i18nService.Translate(token), args));
        }

        protected T Question<T>(string token, string[] allowedAnswers, Func<string, T> answersTransformer, object?[] args = null)
        {
            bool ok = false;

            while (!ok)
            {
                Display(token, args);

                var answer = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(answer))
                    return allowedAnswers.Length > 0 ? answersTransformer(allowedAnswers[0]) : default;

                if(allowedAnswers.Length == 0 || allowedAnswers.Contains(answer))
                {
                    try
                    {
                        return answersTransformer(answer);
                    }
                    catch (Exception) { }
                }

                Display("Did not understand");
            }

            return default;
        }

        protected abstract void DoRun();
    }
}
