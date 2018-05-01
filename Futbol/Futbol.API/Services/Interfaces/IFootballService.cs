using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services.Interfaces
{
    public interface IFootballService : IService
    {
        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <param name="filter">The match filter.</param>
        /// <returns>A list of all matches based on the filters</returns>
        Task<IEnumerable<FootballMatch>> GetMatches(FootballFilter filter);

        /// <summary>
        /// Gets all matches.
        /// </summary>
        /// <returns>A list of all matches</returns>
        Task<IEnumerable<FootballMatch>> GetAllMatches();

        /// <summary>
        /// Gets the competition by identifier.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns>The specified competition</returns>
        Task<FootballCompetition> GetCompetitionById(int competitionId);

        /// <summary>
        /// Gets the competitions.
        /// </summary>
        /// <returns>All competitions</returns>
        Task<IEnumerable<FootballCompetition>> GetCompetitions();

        /// <summary>
        /// Gets the season by identifier.
        /// </summary>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The specified season</returns>
        Task<FootballSeason> GetSeasonById(int seasonId);

        /// <summary>
        /// Gets the seasons.
        /// </summary>
        /// <returns>All seasons</returns>
        Task<IEnumerable<FootballSeason>> GetSeasons();

        /// <summary>
        /// Gets the team by identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>The specified team</returns>
        Task<FootballTeam> GetTeamById(int teamId);

        /// <summary>
        /// Gets the teams.
        /// </summary>
        /// <returns>All teams</returns>
        Task<IEnumerable<FootballTeam>> GetTeams();
    }
}
