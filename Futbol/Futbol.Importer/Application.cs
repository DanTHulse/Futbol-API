using System;
using Futbol.Importer.DataModels.Enums;
using Futbol.Importer.Helpers;
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
            int exit = 0;

            do
            {
                Console.Clear();
                ConsoleLog.Header("Select an option and press Enter");
                ConsoleLog.Options<DataSource>();
                Console.WriteLine();

                var selection = Enum.Parse<DataSource>(ConsoleEx.ReadNumber().ToString());

                Console.Clear();

                switch (selection)
                {
                    case DataSource.FootballDataApi:
                        exit = this.FootballDataApiImport();
                        break;
                    case DataSource.FootballBetData:
                        break;
                    default:
                        exit = 1;
                        break;
                }
            } while (exit == 0);
        }

        /// <summary>
        /// Imports data from the Football Data API.
        /// </summary>
        /// <returns>Exit code</returns>
        private int FootballDataApiImport()
        {
            ConsoleLog.Header($"Football Data API Importer");

            Console.WriteLine($"Start year [{DateTime.Now.Year}]: ");
            var startYear = ConsoleEx.ReadNumber();
            startYear = startYear != 0 ? startYear : DateTime.Now.Year; 

            Console.WriteLine($"End year [{startYear}]: ");
            var endYear = ConsoleEx.ReadNumber();
            endYear = endYear != 0 ? endYear : startYear;

            for (int x = startYear; x <= endYear; x++)
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

            ConsoleLog.Header($"Import complete, do you want to exit? [Y/n]");

            if (Console.Read() == 'n')
            {
                return 1;
            }

            return 0;
        }
    }
}
