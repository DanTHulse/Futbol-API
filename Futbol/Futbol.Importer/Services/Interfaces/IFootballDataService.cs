namespace Futbol.Importer.Services.Interfaces
{
    public interface IFootballDataService : IService
    {
        /// <summary>
        /// Retrieves the competitions by season.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        void RetrieveCompetitionsBySeason(int seasonStartYear);
    }
}
