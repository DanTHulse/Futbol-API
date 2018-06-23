using System;
using Futbol.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Futbol.API.Helpers
{
    /// <summary>
    /// The URL Builder service
    /// </summary>
    /// <seealso cref="Futbol.API.Services.Interfaces.IUrlBuilderService" />
    public class UrlBuilderService : IUrlBuilderService
    {
        /// <summary>
        /// The fb URL
        /// </summary>
        private readonly string FBUrl;

        /// <summary>
        /// The stats URL
        /// </summary>
        private readonly string StatsUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilderService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public UrlBuilderService(IConfigurationRoot config)
        {
            this.FBUrl = config.GetValue<string>("AppSettings:FootballApiUrl");
            this.StatsUrl = config.GetValue<string>("AppSettings:StatsApiUrl");
        }

        /// <summary>
        /// Build the URL for returning all the matches for the scoreline.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The Url for the all matches for the scoreline route</returns>
        public Uri AllMatchesForScoreline(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&competitionId={competitionId}&seasonId={seasonId}");

        /// <summary>
        /// Build the URL for returning the specific match data
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns>The URL for returning the specific match data</returns>
        public Uri MatchData(int matchId)
            => new Uri($"{this.FBUrl}?matchId={matchId}");

        /// <summary>
        /// Build the URL for returning the stats for the scoreline
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns>The URL for returning the stats for the scoreline</returns>
        public Uri ScoreStats(int? firstBoxScore, int? secondBoxScore, int? competitionId, int? seasonId, bool fullTime)
            => new Uri($"{this.StatsUrl}/Scores/{firstBoxScore}/{secondBoxScore}?competitionId={competitionId}&seasonId={seasonId}&fullTime={fullTime}");

        /// <summary>
        /// Build the URL for returning the matches for the scoreline and team
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning the matches for the scoreline and team</returns>
        public Uri AllMatchesForTeamScoreline(int firstBoxScore, int secondBoxScore, int teamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");

        /// <summary>
        /// Build the URL for returning all matches played between the two teams
        /// </summary>
        /// <param name="firstTeamId">The first team identifier.</param>
        /// <param name="secondTeamId">The second team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning all matches played between the two teams</returns>
        public Uri AllMatchesTeams(int firstTeamId, int secondTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?teamId={firstTeamId}&teamId_2={secondTeamId}&competitionId={competitionId}&seasonId={seasonId}");

        /// <summary>
        /// Build the URL for returning the stats for the fixture
        /// </summary>
        /// <param name="homeTeamId">The home team identifier.</param>
        /// <param name="awayTeamId">The away team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The URL for returning the stats for the fixture</returns>
        public Uri FixtureStats(int homeTeamId, int awayTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.StatsUrl}/{homeTeamId}/{awayTeamId}?competitionId={competitionId}&seasonId={seasonId}");
    }
}
