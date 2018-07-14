using System.Collections.Generic;
using Futbol.Common.Models.DataModels;

namespace Futbol.API.Services.Interfaces
{
    public interface IFootballService : IService
    {
        IEnumerable<FootballMatch> GetMatches(FootballFilter filter);

        IEnumerable<FootballMatch> GetMatchById(int matchId);

        FootballCompetition GetCompetitionById(int competitionId);

        IEnumerable<FootballCompetition> GetCompetitions();

        FootballSeason GetSeasonById(int seasonId);

        IEnumerable<FootballSeason> GetSeasons();

        FootballTeam GetTeamById(int teamId);

        IEnumerable<FootballTeam> GetTeams();
    }
}
