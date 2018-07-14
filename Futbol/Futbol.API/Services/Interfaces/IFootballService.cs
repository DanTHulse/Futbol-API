using System.Collections.Generic;
using System.Threading.Tasks;
using Futbol.Common.Models.DataModels;

namespace Futbol.API.Services.Interfaces
{
    public interface IFootballService : IService
    {
        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <param name="filter">The match filter.</param>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A list of all matches based on the filters</returns>
        Task<IEnumerable<FootballMatch>> GetMatches(FootballFilter filter, int page, int pageSize);

        /// <summary>
        /// Gets the match by identifier.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<FootballMatch>> GetMatchById(int matchId);

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
