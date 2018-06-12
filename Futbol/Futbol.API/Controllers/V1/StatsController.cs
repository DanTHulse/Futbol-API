using System.Threading.Tasks;
using Futbol.API.DataModels.Stats;
using Futbol.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbol.API.Controllers.V1
{
    [Route("api/v1/Stats")]
    public class StatsController : Controller
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
        [Route("Scorigami/")]
        [HttpGet]
        [Produces(typeof(StatsScorigami))]
        public async Task<IActionResult> RetrieveScorigami(
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
        [Route("Scores/{firstBoxScore}/{secondBoxScore}")]
        [HttpGet]
        [Produces(typeof(StatsScores))]
        public async Task<IActionResult> RetrieveScoreStats(
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
        [Route("Teams/{teamId}")]
        [HttpGet]
        [Produces(typeof(StatsTeam))]
        public async Task<IActionResult> RetrieveTeamStats([FromRoute]int teamId, [FromQuery]int? competitionId = null, [FromQuery]int? seasonId = null)
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
        [Route("Fixtures/{homeTeam}/{awayTeam}")]
        [HttpGet]
        [Produces(typeof(StatsFixtures))]
        public async Task<IActionResult> RetrieveFixtureStats([FromRoute]int homeTeam, [FromRoute]int awayTeam, [FromQuery]int? competitionId = null, [FromQuery]int? seasonId = null)
        {
            var stats = await Task.Run(() => this.statsService.RetrieveFixturesStats(homeTeam, awayTeam, competitionId, seasonId));

            return this.Ok(stats);
        }
    }
}
