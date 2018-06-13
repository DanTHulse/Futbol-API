using Futbol.Importer.Helpers;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class EnglishSoccorDataService : IEnglishSoccorDataService
    {
        private readonly IFutbolService futbolService;

        private readonly IEnglishSoccorDataService englishSoccorDataService;

        public EnglishSoccorDataService(IFutbolService futbolService, IEnglishSoccorDataService englishSoccorDataService)
        {
            this.englishSoccorDataService = englishSoccorDataService;
            this.futbolService = futbolService;
        }

        /// <summary>
        /// Imports the bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="competitionName">The name of the competition being imported</param>
        /// <param name="seasonStart">The start year of the season period</param>
        public void ImportData(string fileName)
        {
            ConsoleLog.Start($"Parsing fixtures: {competitionName} - {seasonStart}");

            var fixtures = this.footballBetDataRepository.ParseFootballBetData(fileName);

            if (fixtures != null && fixtures.Any())
            {
                ConsoleLog.Information("Fixtures found:", $"{fixtures.Count()}");

                fixtures.Select(s => { s.Division = competitionName; s.Season = seasonStart.ToString(); return s; });

                this.MapRecords(fixtures, competitionName, seasonStart);
            }
            else
            {
                ConsoleLog.Error($"No fixtures found for file: {fileName}", $"{competitionName} - {seasonStart}");
            }
        }
    }
}
