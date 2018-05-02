using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFootballDataRepository : IRepository
    {
        /// <summary>
        /// Retrives the competitions by season.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        /// <returns></returns>
        List<Competitions> RetriveCompetitionsBySeason(int seasonStartYear);

        /// <summary>
        /// Retrieves the fixtures for competition.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns></returns>
        FixturesHeader RetrieveFixturesForCompetition(int competitionId);
    }
}
