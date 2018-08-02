using System.Collections.Generic;
using Futbol.Common.Models.DataModels;
using Futbol.Common.Models.Football;
using Futbol.Common.Models.Stats;

namespace Futbol.API.Repositories.Interfaces
{
    public interface IFutbolRepository : IRepository
    {
        IEnumerable<Match> RetrieveMatches(FootballFilter filter, int pageNumber, int pageSize);

        IEnumerable<Match> RetrieveMatchesByScore(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        IEnumerable<Match> RetrieveMatchesByTeam(int teamId, int? competitionId, int? seasonId);

        IEnumerable<Match> RetrieveMatchesByFixture(int homeTeam, int awayTeam, int? competitionId, int? seasonId);

        Match RetrieveMatchById(int Id);

        IEnumerable<MatchData> RetrieveMatchData(int? competitionId, int? seasonId, bool fullTime);

        IEnumerable<FootballCompetitionSeasons> GetCompetitionSeasons(int competitionId);

        IEnumerable<FootballSeasonCompetition> GetSeasonCompetitions(int seasonId);

        IEnumerable<FootballCompetitionSeasonsTeam_Data> GetCompetitionSeasonsByTeam(int teamId);

        IEnumerable<ScorigamiScores> RetrieveScorigami(int? competitionId, int? seasonId);

        T GetById<T>(int Id) where T : class;

        IEnumerable<T> Get<T>() where T : class;
    }
}
