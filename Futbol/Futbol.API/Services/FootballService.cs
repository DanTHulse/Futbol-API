using System.Collections.Generic;
using System.Linq;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Futbol.Common.Models.DataModels;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services
{
    public class FootballService : IFootballService
    {
        private readonly IFutbolRepository futbolRepository;

        private readonly IUrlBuilderService urlService;

        public FootballService(IFutbolRepository futbolRepository, IUrlBuilderService urlService)
        {
            this.futbolRepository = futbolRepository;
            this.urlService = urlService;
        }

        public IEnumerable<FootballMatch> GetMatches(FootballFilter filter)
        {
            var matchData = this.futbolRepository.RetrieveMatches(filter);
            var mappedMatches = this.MapMatches(matchData);

            return mappedMatches;
        }

        public IEnumerable<FootballMatch> GetMatchById(int matchId)
        {
            var match = this.futbolRepository.RetrieveMatchById(matchId);

            if (match == null || match.MatchId == 0)
            {
                return null;
            }

            return new List<FootballMatch> { this.MapMatchData(match) };
        }

        public FootballCompetition GetCompetitionById(int competitionId)
        {
            var competition = this.futbolRepository.GetById<Competition>(competitionId);
            var seasons = this.futbolRepository.GetCompetitionSeasons(competitionId);

            return new FootballCompetition
            {
                CompetitionId = competition.CompetitionId,
                CompetitionName = competition.CompetitionName,
                Country = competition.Country,
                Tier = competition.Tier,
                Reference = this.urlService.CompetitionReference(competition.CompetitionId),
                AllMatches = this.urlService.AllMatchesCompetition(competition.CompetitionId),
                Seasons = seasons.Select(s =>
                    {
                        s.AllMatches = this.urlService.AllMatchesCompetitionSeason(competition.CompetitionId, s.SeasonId);
                        s.Stats = this.urlService.CompetitionSeasonStats(competition.CompetitionId, s.SeasonId);
                        return s;
                    })
            };
        }

        public IEnumerable<FootballCompetition> GetCompetitions()
        {
            var competitions = this.futbolRepository.Get<Competition>();
            return competitions.Select(s => new FootballCompetition
            {
                CompetitionId = s.CompetitionId,
                CompetitionName = s.CompetitionName,
                Country = s.Country,
                Tier = s.Tier,
                Reference = this.urlService.CompetitionReference(s.CompetitionId),
                AllMatches = this.urlService.AllMatchesCompetition(s.CompetitionId)
            });
        }

        public FootballSeason GetSeasonById(int seasonId)
        {
            var season = this.futbolRepository.GetById<Season>(seasonId);
            return new FootballSeason
            {
                SeasonId = season.SeasonId,
                SeasonPeriod = season.SeasonPeriod,
                Reference = this.urlService.SeasonReference(season.SeasonId),
                AllMatches = this.urlService.AllMatchesSeason(season.SeasonId)
            };
        }

        public IEnumerable<FootballSeason> GetSeasons()
        {
            var seasons = this.futbolRepository.Get<Season>();
            return seasons.Select(s => new FootballSeason
            {
                SeasonId = s.SeasonId,
                SeasonPeriod = s.SeasonPeriod,
                Reference = this.urlService.SeasonReference(s.SeasonId),
                AllMatches = this.urlService.AllMatchesSeason(s.SeasonId)
            });
        }

        public FootballTeam GetTeamById(int teamId)
        {
            var team = this.futbolRepository.GetById<Team>(teamId);
            var seasons = this.futbolRepository.GetCompetitionSeasonsByTeam(teamId);

            var groupedSeasons = seasons
                .GroupBy(grp => new { grp.SeasonId, grp.SeasonPeriod })
                .Select(s => new
                {
                    s.Key,
                    Value = s.Select(r => new FootballTeamSeasonCompetitions
                    {
                        CompetitionName = r.CompetitionName,
                        MatchCount = r.MatchCount,
                        Stats = this.urlService.TeamStats(teamId, r.CompetitionId, s.Key.SeasonId),
                        AllMatches = this.urlService.AllMatchesTeam(teamId, r.CompetitionId, s.Key.SeasonId)
                    })
                 });

            return new FootballTeam
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                Reference = this.urlService.TeamReference(team.TeamId),
                Stats = this.urlService.TeamStats(team.TeamId),
                AllMatches = this.urlService.AllMatchesTeam(team.TeamId),
                CompetitionSeasons = groupedSeasons.Select(s => new FootballTeamSeasons
                {
                    SeasonPeriod = s.Key.SeasonPeriod,
                    MatchCount = s.Value.Sum(x => x.MatchCount),
                    Competitions = s.Value,
                    Stats = this.urlService.TeamStats(teamId, null, s.Key.SeasonId),
                    AllMatches = this.urlService.AllMatchesTeam(teamId, null, s.Key.SeasonId)
                })
            };
        }

        public IEnumerable<FootballTeam> GetTeams()
        {
            var teams = this.futbolRepository.Get<Team>();

            return teams.Select(s => new FootballTeam
            {
                TeamId = s.TeamId,
                TeamName = s.TeamName,
                Reference = this.urlService.TeamReference(s.TeamId),
                Stats = this.urlService.TeamStats(s.TeamId),
                AllMatches = this.urlService.AllMatchesTeam(s.TeamId)
            });
        }

        #region Private Methods

        private IEnumerable<FootballMatch> MapMatches(IEnumerable<Match> matches)
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
                FixtureStats = this.urlService.FixtureStats(s.HomeTeamId, s.AwayTeamId, null, null),
                Reference = this.urlService.MatchReference(s.MatchId)
            }).ToList();
        }

        private FootballMatch MapMatchData(Match match)
        {
            return new FootballMatch
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                MatchName = $"{match.HomeTeam.TeamName} v {match.AwayTeam.TeamName}",
                BoxScore = $"{match.MatchData.FTHomeGoals}-{match.MatchData.FTAwayGoals}",
                Result = $"{match.MatchData.FTResult}",
                HalfTimeBoxScore = $"{match.MatchData.HTHomeGoals}-{match.MatchData.HTAwayGoals}",
                HalfTimeResult = $"{match.MatchData.HTResult}",
                Competition = $"{match.Competition.CompetitionName} {match.Season.SeasonPeriod}",
                FixtureStats = this.urlService.FixtureStats(match.HomeTeamId, match.AwayTeamId, null, null),
                Reference = this.urlService.MatchReference(match.MatchId),
                MatchData = new FootballMatchData
                {
                    HomeTeam = match.HomeTeam.TeamName,
                    AwayTeam = match.AwayTeam.TeamName,
                    HomeTeamId = match.HomeTeamId,
                    AwayTeamId = match.AwayTeamId,
                    CompetitionId = match.CompetitionId,
                    SeasonId = match.SeasonId,
                    FullTimeHomeGoals = match.MatchData.FTHomeGoals,
                    FullTimeAwayGoals = match.MatchData.FTAwayGoals,
                    HalfTimeHomeGoals = match.MatchData.HTHomeGoals,
                    HalfTimeAwayGoals = match.MatchData.HTAwayGoals,
                    HomeShots = match.MatchData.HomeShots,
                    HomeShotsOnTarget = match.MatchData.HomeShotsOnTarget,
                    AwayShots = match.MatchData.AwayShots,
                    AwayShotsOnTarget = match.MatchData.AwayShotsOnTarget,
                    HomeTeamReference = this.urlService.TeamReference(match.HomeTeamId),
                    AwayTeamReference = this.urlService.TeamReference(match.AwayTeamId),
                    CompetitionReference = this.urlService.CompetitionReference(match.CompetitionId),
                    SeasonReference = this.urlService.SeasonReference(match.SeasonId),
                    ScoreStats = match.MatchData.FTGoals_1 == null ? null : this.urlService.ScoreStats(match.MatchData.FTGoals_1, match.MatchData.FTGoals_2, null, null, true),
                    HalfTimeScoreStats = match.MatchData.HTGoals_1 == null ? null : this.urlService.ScoreStats(match.MatchData.HTGoals_1, match.MatchData.HTGoals_2, null, null, false),
                }
            };
        }

        #endregion
    }
}
