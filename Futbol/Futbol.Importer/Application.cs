using System;
using System.IO;
using System.Linq;
using Futbol.Importer.DataModels.Enums;
using Futbol.Importer.Helpers;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer
{
    public class Application : IApplication
    {
        private const string PROJECTDATA_DIRECTORY = "C:\\Source\\Repos\\Futbol\\Futbol\\Futbol.Importer\\ProjectData";

        private readonly IFootballDataService footballDataService;

        private readonly IFootballBetDataService footballBetDataService;

        public Application(IFootballDataService footballDataService, IFootballBetDataService footballBetDataService)
        {
            this.footballDataService = footballDataService;
            this.footballBetDataService = footballBetDataService;
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
                        exit = this.FootballBetDataImport();
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
            Console.WriteLine();
            Console.Write($"Start year [{DateTime.Now.Year}]: ");
            var startYear = ConsoleEx.ReadNumber();
            startYear = startYear != 0 ? startYear : DateTime.Now.Year;
            Console.WriteLine();
            Console.Write($"End year [{startYear}]: ");
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

            ConsoleLog.Header($"Import complete, do you want to continue? [Y/n]");

            if (Console.ReadLine() == "n")
            {
                return 1;
            }

            return 0;
        }

        private int FootballBetDataImport()
        {
            var exit = 0;

            do
            {
                ConsoleLog.Header($"Football Bet Data CSV Importer");
                Console.WriteLine();
                Console.WriteLine("Please select a folder below");

                DirectoryInfo[] subFolders = new DirectoryInfo($"{PROJECTDATA_DIRECTORY}\\Futbol\\FootballBetData").GetDirectories("*.*", SearchOption.AllDirectories);

                for (int i = 0; i < subFolders.ToList().Count; i++)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{i} - ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{subFolders[i].Name}");
                }

                Console.WriteLine();
                Console.WriteLine();
                var selection = ConsoleEx.ReadNumber();

                Console.Clear();
                ConsoleLog.Header($"Football Bet Data CSV Importer: {subFolders[selection].Name}");
                Console.WriteLine();
                Console.WriteLine("Please select a file below");

                FileInfo[] files = new DirectoryInfo(subFolders[selection].FullName).GetFiles();

                for (int i = 0; i < files.ToList().Count; i++)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{i} - ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{files[i].Name}");
                }

                Console.WriteLine();
                Console.WriteLine();
                var fileSelection = ConsoleEx.ReadNumber();

                var seasonStart = Int32.Parse(files[fileSelection].Name.Replace(files[fileSelection].Extension, "").Split(" ").Last());

                this.footballBetDataService.ImportBetData(files[fileSelection].FullName, subFolders[selection].Name, seasonStart);

                ConsoleLog.Header($"Import complete, do you want to continue? [Y/n]");
                Console.WriteLine();

                if (Console.ReadLine() == "n")
                {
                    return 1;
                }

            } while (exit == 0);

            return 0;
        }
    }
}
