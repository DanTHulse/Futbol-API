using System.Collections.Generic;
using System.Linq;
using Futbol.API.Repositories.Interfaces;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.DataModels;
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

        #region Retrieve Matches

        public IEnumerable<Match> RetrieveMatches(FootballFilter filter)
        {
            var matches = this.futbolContext.Match
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
                 .OrderBy(o => o.MatchDate).ToList();

            return matches;
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

        #endregion

        public Match RetrieveMatchById(int Id)
        {
            return this.futbolContext.Match
                .Include(i => i.MatchData)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .Include(i => i.Competition)
                .Include(i => i.Season)
                .Where(w => w.MatchId == Id).FirstOrDefault();
        }

        public IEnumerable<FootballCompetitionSeasons> GetCompetitionSeasons(int competitionId)
        {
            var seasons = this.futbolContext.Set<FootballCompetitionSeasons>().FromSql("football.RetrieveCompetitionSeasons @competitionId = {0}", competitionId).ToList();

            return seasons;
        }

        public IEnumerable<FootballCompetitionSeasonsTeam_Data> GetCompetitionSeasonsByTeam(int teamId)
        {
            var seasons = this.futbolContext.Set<FootballCompetitionSeasonsTeam_Data>().FromSql("football.RetrieveCompetitionSeasonsByTeam @teamId = {0}", teamId).ToList();

            return seasons;
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

        public T GetById<T>(int Id) where T : class
        {
            var record = this.futbolContext.Set<T>().Find(Id);

            return record;
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            var records = this.futbolContext.Set<T>().ToList();

            return records;
        }
    }
}
