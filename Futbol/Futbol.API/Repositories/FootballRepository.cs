using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly FutbolContext futbolContext;

        public FootballRepository(FutbolContext futbolContext)
        {
            this.futbolContext = futbolContext;
        }

        public async Task<IEnumerable<Match>> GetMatches(FootballFilter filter)
        {
            var matches = await Task.Run(() => this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(w => (!filter.CompetitionIds.Any() || filter.CompetitionIds.Contains(w.CompetitionId))
                         && (!filter.SeasonIds.Any() || filter.SeasonIds.Contains(w.SeasonId))
                         && (!filter.TeamIds.Any() || (filter.TeamIds.Contains(w.HomeTeamId) || filter.TeamIds.Contains(w.AwayTeamId)))
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
                                    && w.MatchData.HTHomeGoals == filter.HalftimeBoxScoreSecond.Value))).ToList());

            return matches;
        }

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            var matches = await Task.Run(() => this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season).ToList());

            return matches;
        }
    }
}
