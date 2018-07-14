using System.Threading.Tasks;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.Stats;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(200)]
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
        [ProducesResponseType(200)]
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
        [ProducesResponseType(200)]
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
        [ProducesResponseType(200)]
        public async Task<ActionResult<StatsFixtures>> RetrieveFixtureStats([FromRoute]int homeTeam, [FromRoute]int awayTeam, [FromQuery]int? competitionId = null, [FromQuery]int? seasonId = null)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveFixturesStats(homeTeam, awayTeam, competitionId, seasonId));

            return this.Ok(stats);
        }
    }
}
