using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futbol.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbol.API.Controllers.V1
{
    [Route("api/v1")]
    public class FootballController : Controller
    {
        private readonly IFootballService footballService;

        public FootballController(IFootballService footballService)
        {
            this.footballService = footballService;
        }

        [Route("Scores/")]
        [HttpGet]
        [Produces(typeof(IActionResult))]
        public async Task<IActionResult> GetScores(string competitionIds = null, string seasonIds = null, string teamIds = null,
                                                   int? homeTeamId = null, int? awayTeamId = null, string boxScore = null, string halftimeBoxScore = null,
                                                   bool fullData = false)
        {
            if (string.IsNullOrEmpty(competitionIds) && string.IsNullOrEmpty(seasonIds) && string.IsNullOrEmpty(teamIds)
                && !homeTeamId.HasValue && !awayTeamId.HasValue && string.IsNullOrEmpty(boxScore) && string.IsNullOrEmpty(halftimeBoxScore) && !fullData)
            {
                return this.BadRequest($"Making a request with no filters will return a very large amount of data, use a filter. " +
                                       $"If you absolutely want to return all data set ?fullData=true");
            }
            else if (fullData)
            {
                var allMatches = await this.footballService.GetAllMatches();

                return this.Ok(allMatches);
            }

            var matches = await this.footballService.GetMatches(competitionIds, seasonIds, teamIds, homeTeamId, awayTeamId, boxScore, halftimeBoxScore);

            return this.Ok(matches);
        }
    }
}
