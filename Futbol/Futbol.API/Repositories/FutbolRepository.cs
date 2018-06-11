using System.Collections.Generic;
using System.Linq;
using Futbol.API.Repositories.Interfaces;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.Football;
using Microsoft.EntityFrameworkCore;

namespace Futbol.API.Repositories
{
    /// <summary>
    /// The repository for data from the FUTBOL database
    /// </summary>
    /// <seealso cref="Futbol.API.Repositories.Interfaces.IFutbolRepository" />
    public class FutbolRepository : IFutbolRepository
    {
        /// <summary>
        /// The futbol context
        /// </summary>
        private readonly FutbolContext futbolContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FutbolRepository"/> class.
        /// </summary>
        /// <param name="futbolContext">The futbol context.</param>
        public FutbolRepository(FutbolContext futbolContext)
        {
            this.futbolContext = futbolContext;
        }

        /// <summary>
        /// Retrieves the matches by score.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">True if searching for Full Time scores</param>
        /// <returns></returns>
        public IEnumerable<Match> RetrieveMatchesByScore(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime)
        {
            var matches = this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(l => (fullTime && l.MatchData.FTHomeGoals == firstBoxScore && l.MatchData.FTAwayGoals == secondBoxScore)
                          || (!fullTime && l.MatchData.HTHomeGoals == firstBoxScore && l.MatchData.HTAwayGoals == secondBoxScore))
                .Where(w => !competitionId.HasValue || w.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.SeasonId == seasonId.Value)
                .ToList();

            matches.AddRange(this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(l => (fullTime && l.MatchData.FTHomeGoals == secondBoxScore && l.MatchData.FTAwayGoals == firstBoxScore)
                          || (!fullTime && l.MatchData.HTHomeGoals == firstBoxScore && l.MatchData.HTAwayGoals == secondBoxScore))
                .Where(w => !competitionId.HasValue || w.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.SeasonId == seasonId.Value));

            return matches.OrderBy(o => o.MatchDate).ToList();
        }

        /// <summary>
        /// Retrieves the matches by team.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
        public IEnumerable<Match> RetrieveMatchesByTeam(int teamId, int? competitionId, int? seasonId)
        {
            var matches = this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(l => l.HomeTeamId == teamId || l.AwayTeamId == teamId)
                .Where(w => !competitionId.HasValue || w.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.SeasonId == seasonId.Value);

            return matches.OrderBy(o => o.MatchDate).ToList();
        }

        /// <summary>
        /// Retrieves the matches by fixture.
        /// </summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
        public IEnumerable<Match> RetrieveMatchesByFixture(int homeTeam, int awayTeam, int? competitionId, int? seasonId)
        {
            var matches = this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(l => l.HomeTeamId == homeTeam && l.AwayTeamId == awayTeam)
                .Where(w => !competitionId.HasValue || w.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.SeasonId == seasonId.Value);

            return matches.OrderBy(o => o.MatchDate).ToList();
        }

        /// <summary>
        /// Retrieves the match scores.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns></returns>
        public IEnumerable<MatchData> RetrieveMatchData(int? competitionId, int? seasonId, bool fullTime)
        {
            var matchData = this.futbolContext.MatchData
                .Include(i => i.Match)
                .Where(w => !competitionId.HasValue || w.Match.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.Match.SeasonId == seasonId.Value);

            return matchData;
        }
    }
}
