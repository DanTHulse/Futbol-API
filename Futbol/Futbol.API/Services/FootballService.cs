using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services
{
    public class FootballService : IFootballService
    {
        private readonly IFootballRepository footballRepository;

        public FootballService(IFootballRepository footballRepository)
        {
            this.footballRepository = footballRepository;
        }

        public async Task<IEnumerable<FootballMatch>> GetMatches(FootballFilter filter, int page, int pageSize)
        {
            var matchData = await this.footballRepository.GetMatches(filter, page, pageSize);
            var mappedMatches = this.MapData(matchData);

            //var result = mappedMatches.BuildPageHeader(page, pageSize);

            return mappedMatches;
        }

        public async Task<IEnumerable<FootballMatch>> GetMatchById(int matchId)
        {
            var match = await this.footballRepository.GetMatchById(matchId);

            if (match == null || match.MatchId == 0)
            {
                return null;
            }

            return this.MapData(new List<Match> { match });
        }

        public async Task<FootballCompetition> GetCompetitionById(int competitionId)
        {
            var competition = await this.footballRepository.GetById<Competition>(competitionId);
            return new FootballCompetition { CompetitionId = competition.CompetitionId, CompetitionName = competition.CompetitionName, Country = competition.Country };
        }

        public async Task<IEnumerable<FootballCompetition>> GetCompetitions()
        {
            var competitions = await this.footballRepository.Get<Competition>();
            return competitions.Select(s => new FootballCompetition { CompetitionId = s.CompetitionId, CompetitionName = s.CompetitionName, Country = s.Country });
        }

        public async Task<FootballSeason> GetSeasonById(int seasonId)
        {
            var season = await this.footballRepository.GetById<Season>(seasonId);
            return new FootballSeason { SeasonId = season.SeasonId, SeasonPeriod = season.SeasonPeriod };
        }

        public async Task<IEnumerable<FootballSeason>> GetSeasons()
        {
            var seasons = await this.footballRepository.Get<Season>();
            return seasons.Select(s => new FootballSeason { SeasonId = s.SeasonId, SeasonPeriod = s.SeasonPeriod });
        }

        public async Task<FootballTeam> GetTeamById(int teamId)
        {
            var team = await this.footballRepository.GetById<Team>(teamId);
            return new FootballTeam { TeamId = team.TeamId, TeamName = team.TeamName };
        }

        public async Task<IEnumerable<FootballTeam>> GetTeams()
        {
            var teams = await this.footballRepository.Get<Team>();
            return teams.Select(s => new FootballTeam { TeamId = s.TeamId, TeamName = s.TeamName });
        }

        #region Private Methods

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
