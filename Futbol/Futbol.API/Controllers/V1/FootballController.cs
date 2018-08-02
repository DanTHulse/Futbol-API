using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbol.API.Helpers;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Futbol.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/Football")]
    public class FootballController : ControllerBase
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
        /// Search for football match data.
        /// </summary>
        /// <param name="filter">The match filters</param>
        /// <param name="pageNumber">The current page number</param>
        /// <param name="pageSize">The size of the page to be returned</param>
        /// <returns>A list of matches based on the provided filters</returns>
        [HttpGet("Matches/")]
        [SwaggerResponse(200, "List of Football Matches", typeof(IEnumerable<FootballMatch>))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<FootballMatch>>> SearchScores([FromQuery]FootballFilter filter = null, int pageNumber = 1, int pageSize = 100)
        {
            if (filter != null && filter.MatchId.HasValue)
            {
                return this.Ok(await Task.Run(() => this.footballService.GetMatchById(filter.MatchId.Value)));
            }

            var matches = await Task.Run(() => this.footballService.GetMatches(filter, pageNumber, pageSize));

            return this.Ok(matches);
        }

        /// <summary>
        /// Gets all competitions.
        /// </summary>
        /// <param name="competitionName">[Optional] Search string for competition names, accepts [*] and [?] wildcards</param>
        /// <returns>All competitions</returns>
        [HttpGet("Competitions/")]
        [SwaggerResponse(200, "List of Competitions", typeof(IEnumerable<FootballCompetition>))]
        public async Task<ActionResult<IEnumerable<FootballCompetition>>> GetCompetitions([FromQuery]string competitionName = null)
        {
            var competitions = await Task.Run(() => this.footballService.GetCompetitions());

            if (!string.IsNullOrEmpty(competitionName) && (!competitionName.Contains("*") && !competitionName.Contains("?")))
                competitionName = "*" + competitionName + "*";

            var results = competitions
                .Where(w => string.IsNullOrEmpty(competitionName) || w.CompetitionName.ToLowerInvariant().MatchWildcardString(competitionName.ToLowerInvariant()))
                .OrderBy(o => o.CompetitionName);

            return this.Ok(results);
        }

        /// <summary>
        /// Gets the competition by its identifier.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns>The specific competition</returns>
        [HttpGet("Competitions/{competitionId:int}")]
        [SwaggerResponse(200, "Competition", typeof(FootballCompetition))]
        [SwaggerResponse(204)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<FootballCompetition>> GetCompetitionById([FromRoute]int competitionId)
        {
            var competition = await Task.Run(() => this.footballService.GetCompetitionById(competitionId));

            if (competition == null)
            {
                return this.NoContent();
            }

            return this.Ok(competition);
        }

        /// <summary>
        /// Gets all seasons.
        /// </summary>
        /// <param name="seasonYear">[Optional] The year the season takes place in</param>
        /// <returns>All seasons</returns>
        [HttpGet("Seasons/")]
        [SwaggerResponse(200, "List of Seasons", typeof(IEnumerable<FootballSeason>))]
        public async Task<ActionResult<IEnumerable<FootballSeason>>> GetSeasons([FromQuery]int? seasonYear = null)
        {
            var seasons = await Task.Run(() => this.footballService.GetSeasons());

            var results = seasons
                .Where(w => !seasonYear.HasValue || w.SeasonPeriod.Contains(seasonYear.ToString()))
                .OrderByDescending(o => o.SeasonPeriod);

            return this.Ok(results);
        }

        /// <summary>
        /// Gets the season by its identifier.
        /// </summary>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The specific season</returns>
        [HttpGet("Seasons/{seasonId:int}")]
        [SwaggerResponse(200, "Season", typeof(FootballSeason))]
        [SwaggerResponse(204)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<FootballSeason>> GetSeasonById([FromRoute]int seasonId)
        {
            var season = await Task.Run(() => this.footballService.GetSeasonById(seasonId));

            if (season == null)
            {
                return this.NoContent();
            }

            return this.Ok(season);
        }

        /// <summary>
        /// Gets all teams.
        /// </summary>
        /// <param name="teamName">[Optional] Search string for team names, accepts [*] and [?] wildcards</param>
        /// <returns>All teams</returns>
        [HttpGet("Teams/")]
        [SwaggerResponse(200, "List of Teams", typeof(IEnumerable<FootballTeam>))]
        public async Task<ActionResult<IEnumerable<FootballTeam>>> GetTeams([FromQuery]string teamName = null)
        {
            var teams = await Task.Run(() => this.footballService.GetTeams());

            if (!string.IsNullOrEmpty(teamName) && (!teamName.Contains("*") && !teamName.Contains("?")))
                teamName = "*" + teamName + "*";

            var results = teams
                .Where(w => string.IsNullOrEmpty(teamName) || w.TeamName.ToLowerInvariant().MatchWildcardString(teamName.ToLowerInvariant()))
                .OrderBy(o => o.TeamName);

            return this.Ok(results);
        }

        /// <summary>
        /// Gets the team by its identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>The specific team</returns>
        [HttpGet("Teams/{teamId:int}")]
        [SwaggerResponse(200, "Team", typeof(FootballTeam))]
        [SwaggerResponse(204)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<FootballTeam>> GetTeamById([FromRoute]int teamId)
        {
            var team = await Task.Run(() => this.footballService.GetTeamById(teamId));

            if (team == null)
            {
                return this.NoContent();
            }

            return this.Ok(team);
        }
    }
}
