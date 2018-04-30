using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.API.Helpers;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services
{
    public class FootballService : IFootballService
    {
        private readonly IFootballRepository footballRepository;

        public FootballService(IFootballRepository footballRepository)
        {
            this.footballRepository = footballRepository;
        }

        public async Task<IEnumerable<Match>> GetMatches(string competitionIds, string seasonIds, string teamIds,
                                                         int? homeTeamId, int? awayTeamId, string boxScore, string halftimeBoxScore)
        {
            var filter = this.BuildFilter(competitionIds, seasonIds, teamIds, homeTeamId, awayTeamId, boxScore, halftimeBoxScore);

            return await this.footballRepository.GetMatches(filter);
        }

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            return await this.footballRepository.GetAllMatches();
        }

        private FootballFilter BuildFilter(string competitionIds, string seasonIds, string teamIds,
                                           int? homeTeamId, int? awayTeamId, string boxScore, string halftimeBoxScore)
        {
            return new FootballFilter
            {
                CompetitionIds = competitionIds.ToIntArray(),
                SeasonIds = seasonIds.ToIntArray(),
                TeamIds = teamIds.ToIntArray(),
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                BoxScoreFirst = boxScore?.ToIntArray()?.First(),
                BoxScoreSecond = boxScore?.ToIntArray()?.Last(),
                HalftimeBoxScoreFirst = halftimeBoxScore?.ToIntArray()?.FirstOrDefault(),
                HalftimeBoxScoreSecond = halftimeBoxScore?.ToIntArray()?.LastOrDefault()
            };
        }
    }
}
