namespace Futbol.Importer.Services.Interfaces
{
    public interface IFootballBetDataService : IService
    {
        /// <summary>
        /// Imports the bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="competitionName">The name of the competition being imported</param>
        /// <param name="seasonStart">The start year of the season period</param>
        void ImportBetData(string fileName, string competitionName, int seasonStart);
    }
}
