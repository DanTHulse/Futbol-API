using Futbol.Common.Models.Stats;

namespace Futbol.API.Services.Interfaces
{
    public interface IStatsService : IService
    {
        StatsScores RetrieveScoreStats(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        StatsScorigami RetrieveAllScoreStats(int? competitionId, int? seasonId, bool fullTime);

        StatsTeam RetrieveTeamStats(int teamId, int? competitionId, int? seasonId);

        StatsFixtures RetrieveFixturesStats(int homeTeam, int awayTeam, int? competitionId, int? seasonId);

        StatsCompetitionSeason RetrieveCompetitionSeason(int competitionId, int seasonId);

        StatsLeagueTable RetrieveStatsLeagueTable(int competitionId, int seasonId);
    }
}
