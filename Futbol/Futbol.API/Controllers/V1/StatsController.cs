using System.Threading.Tasks;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.Stats;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Futbol.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/Stats")]
    public class StatsController : ControllerBase
    {
        /// <summary>
        /// The stats service
        /// </summary>
        private readonly IStatsService statsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsController"/> class.
        /// </summary>
        /// <param name="statsService">The stats service.</param>
        public StatsController(IStatsService statsService)
        {
            this.statsService = statsService;
        }

        /// <summary>
        /// Retrieves Scorigami stats.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns>Scorigami</returns>
        [HttpGet("Scorigami/")]
        [SwaggerResponse(200, "Scorigami", typeof(StatsScorigami))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsScorigami>> RetrieveScorigami(
            [FromQuery]int? competitionId = null,
            [FromQuery]int? seasonId = null,
            [FromQuery]bool fullTime = true)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveAllScoreStats(competitionId, seasonId, fullTime));

            return this.Ok(stats);
        }

        /// <summary>
        /// Retrieves the score stats.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns>Stats based around the provided box score</returns>
        [HttpGet("Scores/{firstBoxScore}/{secondBoxScore}")]
        [SwaggerResponse(200, "Stats for Scores", typeof(StatsScores))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsScores>> RetrieveScoreStats(
            [FromRoute]int firstBoxScore,
            [FromRoute]int secondBoxScore,
            [FromQuery]int? competitionId = null,
            [FromQuery]int? seasonId = null,
            [FromQuery]bool fullTime = true)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveScoreStats(firstBoxScore, secondBoxScore, competitionId, seasonId, fullTime));

            return this.Ok(stats);
        }

        /// <summary>
        /// Retrieves the team stats.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>Stats based around the provided team</returns>
        [HttpGet("Teams/{teamId}")]
        [SwaggerResponse(200, "Stats for Teams", typeof(StatsTeam))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsTeam>> RetrieveTeamStats([FromRoute]int teamId, [FromQuery]int? competitionId = null, [FromQuery]int? seasonId = null)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveTeamStats(teamId, competitionId, seasonId));

            return this.Ok(stats);
        }

        /// <summary>
        /// Retrieves the fixture stats.
        /// </summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>Stats based around the provided fixture between two teams</returns>
        [HttpGet("Fixtures/{homeTeam}/{awayTeam}")]
        [SwaggerResponse(200, "Stats for Fixtures", typeof(StatsFixtures))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsFixtures>> RetrieveFixtureStats([FromRoute]int homeTeam, [FromRoute]int awayTeam, [FromQuery]int? competitionId = null, [FromQuery]int? seasonId = null)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveFixturesStats(homeTeam, awayTeam, competitionId, seasonId));

            return this.Ok(stats);
        }

        /// <summary>
        /// Retrieves the competition season stats.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>Stats based around the competition and season as a whole</returns>
        [HttpGet("Competitions/{competitionId}/Seasons/{seasonId}")]
        [SwaggerResponse(200, "Stats for Competition Seasons", typeof(StatsCompetitionSeason))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsCompetitionSeason>> RetrieveCompetitionSeasonStats([FromRoute]int competitionId, [FromRoute]int seasonId)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveCompetitionSeason(competitionId, seasonId));

            return this.Ok(stats);
        }

        /// <summary>
        /// Retrieves the competition season table stats.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The table of stats for the competition and season</returns>
        [HttpGet("Competitions/{competitionId}/Seasons/{seasonId}/Table")]
        [SwaggerResponse(200, "Table of Stats", typeof(StatsLeagueTable))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<StatsLeagueTable>> RetrieveCompetitionSeasonTableStats([FromRoute]int competitionId, [FromRoute]int seasonId)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveStatsLeagueTable(competitionId, seasonId));

            return this.Ok(stats);
        }
    }
}
