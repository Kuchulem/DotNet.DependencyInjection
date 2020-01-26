using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    interface II18nService
    {
        CultureInfo Culture { get; }

        void SetCulture(CultureInfo culture);

        string Translate(string token);

        string Translate(string token, CultureInfo culture);
    }
}
