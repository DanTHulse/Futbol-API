using System.Collections.Generic;
using Futbol.Common.Models.Football;
using Futbol.Common.Models.Stats;

namespace Futbol.API.Repositories.Interfaces
{
    public interface IFutbolRepository : IRepository
    {
        IEnumerable<Match> RetrieveMatchesByScore(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        IEnumerable<Match> RetrieveMatchesByTeam(int teamId, int? competitionId, int? seasonId);

        IEnumerable<Match> RetrieveMatchesByFixture(int homeTeam, int awayTeam, int? competitionId, int? seasonId);

        IEnumerable<MatchData> RetrieveMatchData(int? competitionId, int? seasonId, bool fullTime);

        IEnumerable<ScorigamiScores> RetrieveScorigami(int? competitionId, int? seasonId);
    }
}
