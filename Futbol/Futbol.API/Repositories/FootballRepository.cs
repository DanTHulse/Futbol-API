using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.API.Repositories.Interfaces;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.Football;
using Microsoft.EntityFrameworkCore;

namespace Futbol.API.Repositories
{
    public class FootballRepository : IFootballRepository
    {
        /// <summary>
        /// The futbol context
        /// </summary>
        private readonly FutbolContext futbolContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballRepository"/> class.
        /// </summary>
        /// <param name="futbolContext">The futbol context.</param>
        public FootballRepository(FutbolContext futbolContext)
        {
            this.futbolContext = futbolContext;
        }

        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A list of all matches based on the filter</returns>
        public async Task<IEnumerable<Match>> GetMatches(FootballFilter filter, int page, int pageSize)
        {
            var matches = await Task.Run(() => this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(w => (!filter.CompetitionId.HasValue || filter.CompetitionId.Value == w.CompetitionId)
                         && (!filter.SeasonId.HasValue || filter.SeasonId.Value == w.SeasonId)
                         && (!filter.TeamId.HasValue || ((filter.TeamId == w.HomeTeamId && (!filter.TeamId_2.HasValue || filter.TeamId_2 == w.AwayTeamId))
                                                     || (filter.TeamId == w.AwayTeamId && (!filter.TeamId_2.HasValue || filter.TeamId_2 == w.HomeTeamId))))
                         && (!filter.HomeTeamId.HasValue || w.HomeTeamId == filter.HomeTeamId.Value)
                         && (!filter.AwayTeamId.HasValue || w.AwayTeamId == filter.AwayTeamId.Value)
                         && (!filter.BoxScoreFirst.HasValue
                                || (w.MatchData.FTHomeGoals == filter.BoxScoreFirst.Value
                                    && w.MatchData.FTAwayGoals == filter.BoxScoreSecond.Value)
                                || (w.MatchData.FTAwayGoals == filter.BoxScoreFirst.Value
                                    && w.MatchData.FTHomeGoals == filter.BoxScoreSecond.Value))
                         && (!filter.HalftimeBoxScoreFirst.HasValue
                                || (w.MatchData.HTHomeGoals == filter.HalftimeBoxScoreFirst.Value
                                    && w.MatchData.HTAwayGoals == filter.HalftimeBoxScoreSecond.Value)
                                || (w.MatchData.HTAwayGoals == filter.HalftimeBoxScoreFirst.Value
                                    && w.MatchData.HTHomeGoals == filter.HalftimeBoxScoreSecond.Value)))
                 .OrderBy(o => o.MatchDate).ToList());

            return matches;
        }

        /// <summary>
        /// Gets the match by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public async Task<Match> GetMatchById(int Id)
        {
            return await Task.Run(() => this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(w => w.MatchId == Id).FirstOrDefault());
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Record by Id</returns>
        public async Task<T> GetById<T>(int Id) where T : class
        {
            var record = await this.futbolContext.Set<T>().FindAsync(Id);

            return record;
        }

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>List of all records</returns>
        public async Task<IEnumerable<T>> Get<T>() where T : class
        {
            var records = await this.futbolContext.Set<T>().ToListAsync();

            return records;
        }
    }
}
