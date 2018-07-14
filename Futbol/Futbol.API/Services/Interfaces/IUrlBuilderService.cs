using System;

namespace Futbol.API.Services.Interfaces
{
    /// <summary>
    /// Interface for the Url Builder service
    /// </summary>
    /// <seealso cref="Futbol.API.Services.Interfaces.IService" />
    public interface IUrlBuilderService : IService
    {
        Uri AllMatchesForScoreline(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId);

        Uri AllMatchesForTeamScoreline(int firstBoxScore, int secondBoxScore, int teamId, int? competitionId, int? seasonId);

        Uri AllMatchesTeams(int firstTeamId, int secondTeamId, int? competitionId, int? seasonId);

        Uri AllMatchesTeam(int teamId, int? competitionId = null, int? seasonId = null);

        Uri AllMatchesCompetition(int competitionId);

        Uri AllMatchesSeason(int seasonId);

        Uri AllMatchesCompetitionSeason(int competitionId, int seasonId);

        Uri ScoreStats(int? firstBoxScore, int? secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        Uri FixtureStats(int homeTeamId, int awayTeamId, int? competitionId, int? seasonId);

        Uri TeamStats(int teamId, int? competitionId = null, int? seasonId = null);

        Uri CompetitionSeasonStats(int competitionId, int seasonId);

        Uri CompetitionReference(int competitionId);

        Uri SeasonReference(int seasonId);

        Uri TeamReference(int teamId);

        Uri MatchReference(int matchId);
    }
}
