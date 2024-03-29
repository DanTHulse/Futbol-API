﻿using System.Collections.Generic;
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
        private readonly FutbolContext futbolContext;

        public FootballRepository(FutbolContext futbolContext)
        {
            this.futbolContext = futbolContext;
        }

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

        public async Task<T> GetById<T>(int Id) where T : class
        {
            var record = await this.futbolContext.Set<T>().FindAsync(Id);

            return record;
        }

        public async Task<IEnumerable<T>> Get<T>() where T : class
        {
            var records = await this.futbolContext.Set<T>().ToListAsync();

            return records;
        }
    }
}
