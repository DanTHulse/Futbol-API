using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.API.Helpers;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services
{
    public class FootballService : IFootballService
    {
        /// <summary>
        /// The football repository
        /// </summary>
        private readonly IFootballRepository footballRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballService"/> class.
        /// </summary>
        /// <param name="footballRepository">The football repository.</param>
        public FootballService(IFootballRepository footballRepository)
        {
            this.footballRepository = footballRepository;
        }

        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <param name="filter">The match filters</param>
        /// <param name="page">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A list of matches based on the filters</returns>
        public async Task<PageHeader<FootballMatch>> GetMatches(FootballFilter filter, int page, int pageSize)
        {
            var matchData = await this.footballRepository.GetMatches(filter, page, pageSize);
            var mappedMatches = this.MapData(matchData);

            var result = mappedMatches.BuildPageHeader(page, pageSize);

            return result;
        }

        /// <summary>
        /// Gets the competition by identifier.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns>The specified competition</returns>
        public async Task<FootballCompetition> GetCompetitionById(int competitionId)
        {
            var competition = await this.footballRepository.GetById<Competition>(competitionId);
            return new FootballCompetition { CompetitionId = competition.CompetitionId, CompetitionName = competition.CompetitionName, Country = competition.Country };
        }

        /// <summary>
        /// Gets the competitions.
        /// </summary>
        /// <returns>All competitions</returns>
        public async Task<IEnumerable<FootballCompetition>> GetCompetitions()
        {
            var competitions = await this.footballRepository.Get<Competition>();
            return competitions.Select(s => new FootballCompetition { CompetitionId = s.CompetitionId, CompetitionName = s.CompetitionName, Country = s.Country });
        }

        /// <summary>
        /// Gets the season by identifier.
        /// </summary>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns>The specified season</returns>
        public async Task<FootballSeason> GetSeasonById(int seasonId)
        {
            var season = await this.footballRepository.GetById<Season>(seasonId);
            return new FootballSeason { SeasonId = season.SeasonId, SeasonPeriod = season.SeasonPeriod };
        }

        /// <summary>
        /// Gets the seasons.
        /// </summary>
        /// <returns>All seasons</returns>
        public async Task<IEnumerable<FootballSeason>> GetSeasons()
        {
            var seasons = await this.footballRepository.Get<Season>();
            return seasons.Select(s => new FootballSeason { SeasonId = s.SeasonId, SeasonPeriod = s.SeasonPeriod });
        }

        /// <summary>
        /// Gets the team by identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>The specified team</returns>
        public async Task<FootballTeam> GetTeamById(int teamId)
        {
            var team = await this.footballRepository.GetById<Team>(teamId);
            return new FootballTeam { TeamId = team.TeamId, TeamName = team.TeamName };
        }

        /// <summary>
        /// Gets the teams.
        /// </summary>
        /// <returns>All teams</returns>
        public async Task<IEnumerable<FootballTeam>> GetTeams()
        {
            var teams = await this.footballRepository.Get<Team>();
            return teams.Select(s => new FootballTeam { TeamId = s.TeamId, TeamName = s.TeamName });
        }

        #region Private Methods

        /// <summary>
        /// Maps the football data.
        /// </summary>
        /// <param name="matches">The matches.</param>
        /// <returns>List of mapped football data</returns>
        private IEnumerable<FootballMatch> MapData(IEnumerable<Match> matches)
        {
            return matches.Select(s => new FootballMatch
            {
                MatchId = s.MatchId,
                MatchDate = s.MatchDate,
                MatchName = $"{s.HomeTeam.TeamName} v {s.AwayTeam.TeamName}",
                BoxScore = $"{s.MatchData.FTHomeGoals}-{s.MatchData.FTAwayGoals}",
                Result = $"{s.MatchData.FTResult}",
                HalfTimeBoxScore = $"{s.MatchData.HTHomeGoals}-{s.MatchData.HTAwayGoals}",
                HalfTimeResult = $"{s.MatchData.HTResult}",
                Competition = $"{s.Competition.CompetitionName} {s.Season.SeasonPeriod}",
                MatchData = new FootballMatchData
                {
                    HomeTeam = s.HomeTeam.TeamName,
                    AwayTeam = s.AwayTeam.TeamName,
                    HomeTeamId = s.HomeTeamId,
                    AwayTeamId = s.AwayTeamId,
                    CompetitionId = s.CompetitionId,
                    SeasonId = s.SeasonId,
                    FullTimeHomeGoals = s.MatchData.FTHomeGoals,
                    FullTimeAwayGoals = s.MatchData.FTAwayGoals,
                    HalfTimeHomeGoals = s.MatchData.HTHomeGoals,
                    HalfTimeAwayGoals = s.MatchData.HTAwayGoals
                }
            }).ToList();
        }

        #endregion
    }
}
