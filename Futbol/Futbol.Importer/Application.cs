using System;
using Futbol.Importer.Helpers;
using Futbol.Importer.Infrastructure;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer
{
    public class Application : IApplication
    {
        private readonly IFootballDataService footballDataService;

        public Application(IFootballDataService footballDataService)
        {
            this.footballDataService = footballDataService;
        }

        public void Run()
        {
            for (int x = 1999; x > 1930; x--)
            {
                try
                {
                    ConsoleLog.Header($"Starting import for Season start: {x}");

                    this.footballDataService.RetrieveCompetitionsBySeason(x);
                }
                catch (Exception ex)
                {
                    ConsoleLog.Error($"Something went wrong", $"{ex.Message}");
                    continue;
                }
            }
        }
    }
}
