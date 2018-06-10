using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbol.API.Controllers.V1
{
    [Route("api/v1/Football")]
    public class FootballController : Controller
    {
        /// <summary>
        /// The football service
        /// </summary>
        private readonly IFootballService footballService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballController"/> class.
        /// </summary>
        /// <param name="footballService">The football service.</param>
        public FootballController(IFootballService footballService)
        {
            this.footballService = footballService;
        }

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <param name="filter">The match filters</param>
        /// <param name="fullData">if set to <c>true</c> [full data].</param>
        /// <returns>A list of matches based on the provided filters</returns>
        [Route("Matches/")]
        [HttpGet]
        [Produces(typeof(IEnumerable<FootballMatch>))]
        public async Task<IActionResult> SearchScores([FromQuery]FootballFilter filter = null, [FromQuery]int page = 1, [FromQuery]int pageSize = 100)
        {
            if (filter != null && filter.MatchId.HasValue)
            {
                return this.Ok(await this.footballService.GetMatchById(filter.MatchId.Value));
            }

            var matches = await this.footballService.GetMatches(filter, page, pageSize);

            return this.Ok(matches);
        }

        /// <summary>
        /// Gets the competitions.
        /// </summary>
        /// <returns>Gets all competitions</returns>
        [Route("Competitions/")]
        [HttpGet]
        [Produces(typeof(IEnumerable<FootballCompetition>))]
        public async Task<IActionResult> GetCompetitions()
        {
            var competitions = await this.footballService.GetCompetitions();

            return this.Ok(competitions.OrderBy(o => o.CompetitionName));
        }

        /// <summary>
        /// Gets the competition by identifier.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns>Get specific competition</returns>
        [Route("Competitions/{competitionId:int}")]
        [HttpGet]
        [Produces(typeof(FootballCompetition))]
        public async Task<IActionResult> GetCompetitionById([FromRoute]int competitionId)
        {
            var competition = await this.footballService.GetCompetitionById(competitionId);

            if (competition == null)
            {
                return this.NoContent();
            }

            return this.Ok(competition);
        }

        /// <summary>
        /// Gets the seasons.
        /// </summary>
        /// <returns>Gets all seasons</returns>
        [Route("Seasons/")]
        [HttpGet]
        [Produces(typeof(IEnumerable<FootballSeason>))]
        public async Task<IActionResult> GetSeasons()
        {
            var seasons = await this.footballService.GetSeasons();

            return this.Ok(seasons.OrderByDescending(o => o.SeasonPeriod));
        }

        /// <summary>
        /// Gets the season by identifier.
        /// </summary>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>Get specific season</returns>
        [Route("Seasons/{seasonId:int}")]
        [HttpGet]
        [Produces(typeof(FootballSeason))]
        public async Task<IActionResult> GetSeasonById([FromRoute]int seasonId)
        {
            var season = await this.footballService.GetSeasonById(seasonId);

            if (season == null)
            {
                return this.NoContent();
            }

            return this.Ok(season);
        }

        /// <summary>
        /// Gets the teams.
        /// </summary>
        /// <returns>Gets all teams</returns>
        [Route("Teams/")]
        [HttpGet]
        [Produces(typeof(IEnumerable<FootballTeam>))]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await this.footballService.GetTeams();

            return this.Ok(teams.OrderBy(o => o.TeamName));
        }

        /// <summary>
        /// Gets the team by identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>Gets specific team</returns>
        [Route("Teams/{teamId:int}")]
        [HttpGet]
        [Produces(typeof(FootballTeam))]
        public async Task<IActionResult> GetTeamById([FromRoute]int teamId)
        {
            var team = await this.footballService.GetTeamById(teamId);

            if (team == null)
            {
                return this.NoContent();
            }

            return this.Ok(team);
        }
    }
}
