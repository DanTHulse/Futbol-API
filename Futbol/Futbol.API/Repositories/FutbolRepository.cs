using System.Collections.Generic;
using System.Linq;
using Futbol.API.Repositories.Interfaces;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.Football;
using Futbol.Common.Models.Stats;
using Microsoft.EntityFrameworkCore;

namespace Futbol.API.Repositories
{
    public class FutbolRepository : IFutbolRepository
    {
        private readonly FutbolContext futbolContext;

        public FutbolRepository(FutbolContext futbolContext)
        {
            this.futbolContext = futbolContext;
        }

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
                .Where(l => ((fullTime && l.MatchData.FTHomeGoals == secondBoxScore && l.MatchData.FTAwayGoals == firstBoxScore)
                          || (!fullTime && l.MatchData.HTHomeGoals == firstBoxScore && l.MatchData.HTAwayGoals == secondBoxScore))
                       && ((fullTime && l.MatchData.FTResult == "A")
                          || !fullTime && l.MatchData.HTResult == "A"))
                .Where(w => !competitionId.HasValue || w.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.SeasonId == seasonId.Value));

            return matches.OrderBy(o => o.MatchDate).ToList();
        }

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

        public IEnumerable<MatchData> RetrieveMatchData(int? competitionId, int? seasonId, bool fullTime)
        {
            var matchData = this.futbolContext.MatchData
                .Include(i => i.Match)
                .Where(w => !competitionId.HasValue || w.Match.CompetitionId == competitionId.Value)
                .Where(w => !seasonId.HasValue || w.Match.SeasonId == seasonId.Value);

            return matchData;
        }

        public IEnumerable<ScorigamiScores> RetrieveScorigami(int? competitionId, int? seasonId)
        {
            if (!competitionId.HasValue)
                competitionId = 0;
            if (!seasonId.HasValue)
                seasonId = 0;

            var data = this.futbolContext.Set<ScorigamiScores>().FromSql("football.RetrieveScorigami @CompetitionId = {0}, @SeasonId = {1}", competitionId, seasonId).ToList();

            return data;
        }
    }
}
