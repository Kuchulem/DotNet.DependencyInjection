using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Demo.Services
{
    class InMemoryI18nService : II18nService
    {
        private CultureInfo defaultCulture;

        private readonly Dictionary<(CultureInfo culture, string token), string> translations;

        public CultureInfo Culture => defaultCulture;

        public InMemoryI18nService()
        {
            translations = new Dictionary<(CultureInfo culture, string token), string>
            {
                { (new CultureInfo("en"), "Welcome"), "Welcome to the demo of Kuchulem's dependency injection !" },
                { (new CultureInfo("fr"), "Welcome"), "Bienvenue sur la démo de l'injection de dépencences de Kuchulem !" },
                { (new CultureInfo("en"), "Language ?"), "In what language do you wish to run the demo ? (EN/fr)" },
                { (new CultureInfo("fr"), "Language ?"), "Dans quelle langue voulez-vous lancer la démo ? (EN/fr)" },
                { (new CultureInfo("en"), "Presentation"), "This demo will display this numerical sequence : {0}" },
                { (new CultureInfo("fr"), "Presentation"), "Cette démo va afficher cette suite numérique : {0}" },
                { (new CultureInfo("en"), "Iterations count ?"), "How many iterations would you like to display for the sequence ? (10)" },
                { (new CultureInfo("fr"), "Iterations count ?"), "Combien d'itérations de la suite voulez-vous afficher ? (10)" },
                { (new CultureInfo("en"), "Continue ?"), "Do you wish to run the demo again ? (Y/n/yes/no)" },
                { (new CultureInfo("fr"), "Continue ?"), "VOulez-vous lancer la démo une nouvelle fois ? (Y/n/yes/no)" },
                { (new CultureInfo("en"), "Did not understand"), "Sorry I did not understand." },
                { (new CultureInfo("fr"), "Did not understand"), "Désolé je n'ai pas compris." },
                { (new CultureInfo("en"), "Goodbye"), "Goodbye. See you soon !" },
                { (new CultureInfo("fr"), "Goodbye"), "Au revoir. A bientôt !" },
                { (new CultureInfo("en"), "DisplaySequenceFormat"), "Running {0}" },
                { (new CultureInfo("fr"), "DisplaySequenceFormat"), "Lancement de {0}" },
                { (new CultureInfo("en"), "SequenceDone"), "Job's done !" },
                { (new CultureInfo("fr"), "SequenceDone"), "Travail terminé !" },
                { (new CultureInfo("en"), "DisplayIterationValueFormat"), "Iteration {0} : {1}" },
                { (new CultureInfo("fr"), "DisplayIterationValueFormat"), "Itération {0} : {1}" },
                { (new CultureInfo("en"), "Fibonacci Sequence"), "Fibonacci Sequence" },
                { (new CultureInfo("fr"), "Fibonacci Sequence"), "Suite de Fibonacci" },
            };
        }

        public void SetCulture(CultureInfo culture)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            defaultCulture = culture;
        }

        public string Translate(string token)
        {
            return Translate(token, defaultCulture ?? CultureInfo.InvariantCulture);
        }

        public string Translate(string token, CultureInfo culture)
        {
            var tokenWithCulture = (culture, token);
            if (translations.ContainsKey(tokenWithCulture))
            {
                return translations[tokenWithCulture];
            }

            return token;
        }
    }
}
