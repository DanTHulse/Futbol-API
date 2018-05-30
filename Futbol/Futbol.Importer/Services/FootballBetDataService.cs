using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class FootballBetDataService : IFootballBetDataService
    {
        /// <summary>
        /// The football bet data repository
        /// </summary>
        private readonly IFootballBetDataRepository footballBetDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballBetDataService"/> class.
        /// </summary>
        /// <param name="footballDataRepository">The football data repository.</param>
        public FootballBetDataService(IFootballBetDataRepository footballBetDataRepository)
        {
            this.footballBetDataRepository = footballBetDataRepository;
        }

        /// <summary>
        /// Imports the bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void ImportBetData(string fileName, string competitionName)
        {
            var matches = this.footballBetDataRepository.ParseFootballBetData(fileName, competitionName);
        }

        private void MapRecords(object matchData)
        {

        }
    }
}
