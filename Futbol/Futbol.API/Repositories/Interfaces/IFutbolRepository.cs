using System.Collections.Generic;
using Futbol.Common.Models.Football;

namespace Futbol.API.Repositories.Interfaces
{
    public interface IFutbolRepository : IRepository
    {
        /// <summary>
        /// Retrieves the matches by score.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">True if searching for Full Time scores</param>
        /// <returns></returns>
        IEnumerable<Match> RetrieveMatchesByScore(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        /// <summary>
        /// Retrieves the matches by team.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
        IEnumerable<Match> RetrieveMatchesByTeam(int teamId, int? competitionId, int? seasonId);

        /// <summary>
        /// Retrieves the matches by fixture.
        /// </summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
        IEnumerable<Match> RetrieveMatchesByFixture(int homeTeam, int awayTeam, int? competitionId, int? seasonId);

        /// <summary>
        /// Retrieves the match scores.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns></returns>
        IEnumerable<MatchData> RetrieveMatchData(int? competitionId, int? seasonId, bool fullTime);
    }
}
