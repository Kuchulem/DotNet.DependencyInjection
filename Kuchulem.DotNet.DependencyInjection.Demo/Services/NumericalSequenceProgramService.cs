using Kuchulem.DotNet.DependencyInjection.Writer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    class NumericalSequenceProgramService : BaseProgramService, IProgramService
    {
        private readonly INumericalSequenceService sequenceService;

        public NumericalSequenceProgramService(INumericalSequenceService sequenceService, II18nService i18nService, IWriter writer)
            : base(writer)
        {
            this.sequenceService = sequenceService;
            this.i18nService = i18nService;
        }
        protected override void DoRun()
        {
            Display("Presentation", new[] { sequenceService.SequenceName });

            var nbIterations = Question("Iterations count ?", new string[] { }, (string answer) => int.Parse(answer, CultureInfo.InvariantCulture));

            if (nbIterations == default)
                nbIterations = 10;

            Display("DisplaySequenceFormat", new[] { sequenceService.SequenceName });

            var index = 0;

            foreach(var value in sequenceService.InitialValues)
            {
                if (index > nbIterations)
                    break;
                index++;
                DisplayValue(index, value);
            }

            for (var i = index; i < nbIterations; i++)
                DisplayValue(i + 1, sequenceService.Next());

            writer.WriteLine(i18nService.Translate("SequenceDone"));
        }

        private void DisplayValue(int valueIndex, int value)
        {
            writer.WriteLine(string.Format(
                i18nService.Translate("DisplayIterationValueFormat"),
                valueIndex,
                value
            ));
        }
    }
}
