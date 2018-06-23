using System;

namespace Futbol.API.Services.Interfaces
{
    /// <summary>
    /// Interface for the Url Builder service
    /// </summary>
    /// <seealso cref="Futbol.API.Services.Interfaces.IService" />
    public interface IUrlBuilderService : IService
    {
        /// <summary>
        /// Build the URL for returning all the matches for the scoreline.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The Url for the all matches for the scoreline route</returns>
        Uri AllMatchesForScoreline(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId);

        /// <summary>
        /// Build the URL for returning the specific match data
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns>The URL for returning the specific match data</returns>
        Uri MatchData(int matchId);

        /// <summary>
        /// Build the URL for returning the stats for the scoreline
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns>The URL for returning the stats for the scoreline</returns>
        Uri ScoreStats(int? firstBoxScore, int? secondBoxScore, int? competitionId, int? seasonId, bool fullTime);

        /// <summary>
        /// Build the URL for returning the matches for the scoreline and team
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning the matches for the scoreline and team</returns>
        Uri AllMatchesForTeamScoreline(int firstBoxScore, int secondBoxScore, int teamId, int? competitionId, int? seasonId);

        /// <summary>
        /// Build the URL for returning all matches played between the two teams
        /// </summary>
        /// <param name="firstTeamId">The first team identifier.</param>
        /// <param name="secondTeamId">The second team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning all matches played between the two teams</returns>
        Uri AllMatchesTeams(int firstTeamId, int secondTeamId, int? competitionId, int? seasonId);

        /// <summary>
        /// Build the URL for returning the stats for the fixture
        /// </summary>
        /// <param name="homeTeamId">The home team identifier.</param>
        /// <param name="awayTeamId">The away team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning the stats for the fixture</returns>
        Uri FixtureStats(int homeTeamId, int awayTeamId, int? competitionId, int? seasonId);
    }
}
