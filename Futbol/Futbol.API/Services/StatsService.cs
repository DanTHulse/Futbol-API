using System.Linq;
using Futbol.API.DataModels.Enumerations;
using Futbol.API.DataModels.Stats;
using Futbol.API.Helpers;
using Futbol.API.Repositories.Interfaces;
using Futbol.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Futbol.API.Services
{
    public class StatsService : IStatsService
    {
        private readonly IFutbolRepository futbolRepository;

        private readonly IUrlBuilderService urlService;

        public StatsService(IFutbolRepository futbolRepository, IUrlBuilderService urlService, IConfigurationRoot configuration)
        {
            this.futbolRepository = futbolRepository;
            this.urlService = urlService;
        }

        public StatsScores RetrieveScoreStats(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId, bool fullTime)
        {
            var matches = this.futbolRepository.RetrieveMatchesByScore(firstBoxScore, secondBoxScore, competitionId, seasonId, fullTime);

            if (matches == null || !matches.Any())
            {
                return null;
            }

            var firstMatch = matches.FirstOrDefault();
            var lastMatch = matches.LastOrDefault();

            var scoreStats = new StatsScores
            {
                BoxScore = $"{firstBoxScore}-{secondBoxScore}",
                FirstMatch = firstMatch.BuildResults(),
                LastMatch = lastMatch.BuildResults(),
                Count = matches.Count(),
                AllMatches = this.urlService.AllMatchesForScoreline(firstBoxScore, secondBoxScore, competitionId, seasonId)
            };

            scoreStats.FirstMatch.MatchData = this.urlService.MatchData(scoreStats.FirstMatch.MatchId);
            scoreStats.LastMatch.MatchData = this.urlService.MatchData(scoreStats.LastMatch.MatchId);

            return scoreStats;
        }

        public StatsScorigami RetrieveAllScoreStats(int? competitionId, int? seasonId, bool fullTime)
        {
            var matchData = this.futbolRepository.RetrieveScorigami(competitionId, seasonId);

            var stats = matchData
                .Where(w => w.Score_1.HasValue)
                .Select(s => { s.ScoreStats = this.urlService.ScoreStats(s.Score_1, s.Score_2, competitionId, seasonId, fullTime); return s; });

            return new StatsScorigami { Scores = stats };
        }

        public StatsTeam RetrieveTeamStats(int teamId, int? competitionId, int? seasonId)
        {
            var matches = this.futbolRepository.RetrieveMatchesByTeam(teamId, competitionId, seasonId);

            if (matches == null || !matches.Any())
            {
                return null;
            }

            var team = matches.FirstOrDefault(f => f.HomeTeamId == teamId).HomeTeam;

            var wins = matches.SplitMatchesByResult(teamId, Result.Win);
            var losses = matches.SplitMatchesByResult(teamId, Result.Loss);
            var draws = matches.SplitMatchesByResult(teamId, Result.Draw);

            var teamStats = new StatsTeam
            {
                TeamName = team.TeamName,
                BiggestWin = wins.all.CalculateBiggestResult(),
                BiggestLoss = losses.all.CalculateBiggestResult(),
                BiggestDraw = draws.all.CalculateBiggestResult(),
                Record = new StatsRecord
                {
                    GamesWon = wins.all.Count(),
                    GamesLost = losses.all.Count(),
                    GamesDrawn = draws.all.Count(),
                    GoalsFor = (wins.all.Sum(s => s.MatchData.FTGoals_1) + losses.all.Sum(s => s.MatchData.FTGoals_2) + draws.all.Sum(s => s.MatchData.FTGoals_1)).Value,
                    GoalsAgainst = (wins.all.Sum(s => s.MatchData.FTGoals_2) + losses.all.Sum(s => s.MatchData.FTGoals_1) + draws.all.Sum(s => s.MatchData.FTGoals_1)).Value
                },
                HomeRecord = new StatsRecord
                {
                    GamesWon = wins.home.Count(),
                    GamesLost = losses.home.Count(),
                    GamesDrawn = draws.home.Count(),
                    GoalsFor = (wins.home.Sum(s => s.MatchData.FTGoals_1) + losses.home.Sum(s => s.MatchData.FTGoals_2) + draws.home.Sum(s => s.MatchData.FTGoals_1)).Value,
                    GoalsAgainst = (wins.home.Sum(s => s.MatchData.FTGoals_2) + losses.home.Sum(s => s.MatchData.FTGoals_1) + draws.home.Sum(s => s.MatchData.FTGoals_1)).Value
                },
                AwayRecord = new StatsRecord
                {
                    GamesWon = wins.away.Count(),
                    GamesLost = losses.away.Count(),
                    GamesDrawn = draws.away.Count(),
                    GoalsFor = (wins.away.Sum(s => s.MatchData.FTGoals_1) + losses.away.Sum(s => s.MatchData.FTGoals_2) + draws.away.Sum(s => s.MatchData.FTGoals_1)).Value,
                    GoalsAgainst = (wins.away.Sum(s => s.MatchData.FTGoals_2) + losses.away.Sum(s => s.MatchData.FTGoals_1) + draws.away.Sum(s => s.MatchData.FTGoals_1)).Value
                },
                MostGamesPlayedAgainst = matches.CalculateMostPlayed(team.TeamId),
                MostWinsAgainst = wins.all.CalculateMostPlayed(team.TeamId),
                MostLossesAgainst = losses.all.CalculateMostPlayed(team.TeamId),
                MostDrawsAgainst = draws.all.CalculateMostPlayed(team.TeamId)
            };

            teamStats.BiggestWin.FirstMatch.MatchData = this.urlService.MatchData(teamStats.BiggestWin.FirstMatch.MatchId);
            teamStats.BiggestWin.LastMatch.MatchData = this.urlService.MatchData(teamStats.BiggestWin.LastMatch.MatchId);

            teamStats.BiggestLoss.FirstMatch.MatchData = this.urlService.MatchData(teamStats.BiggestLoss.FirstMatch.MatchId);
            teamStats.BiggestLoss.LastMatch.MatchData = this.urlService.MatchData(teamStats.BiggestLoss.LastMatch.MatchId);

            teamStats.BiggestDraw.FirstMatch.MatchData = this.urlService.MatchData(teamStats.BiggestDraw.FirstMatch.MatchId);
            teamStats.BiggestDraw.LastMatch.MatchData = this.urlService.MatchData(teamStats.BiggestDraw.LastMatch.MatchId);

            teamStats.MostGamesPlayedAgainst.Select(s => { s.AllMatches = this.urlService.AllMatchesTeams(s.Team_1, s.Team_2, competitionId, seasonId); return s; }).ToList();
            teamStats.MostWinsAgainst.Select(s => { s.AllMatches = this.urlService.AllMatchesTeams(s.Team_1, s.Team_2, competitionId, seasonId); return s; }).ToList();
            teamStats.MostLossesAgainst.Select(s => { s.AllMatches = this.urlService.AllMatchesTeams(s.Team_1, s.Team_2, competitionId, seasonId); return s; }).ToList();
            teamStats.MostDrawsAgainst.Select(s => { s.AllMatches = this.urlService.AllMatchesTeams(s.Team_1, s.Team_2, competitionId, seasonId); return s; }).ToList();

            return teamStats;
        }

        public StatsFixtures RetrieveFixturesStats(int homeTeam, int awayTeam, int? competitionId, int? seasonId)
        {
            var matches = this.futbolRepository.RetrieveMatchesByFixture(homeTeam, awayTeam, competitionId, seasonId);

            if (matches == null || !matches.Any())
            {
                return null;
            }

            var firstMatch = matches.First();

            var fixtureStats = new StatsFixtures
            {
                HomeTeam = firstMatch.HomeTeam.TeamName,
                AwayTeam = firstMatch.AwayTeam.TeamName,
                FirstResult = firstMatch.BuildResults(),
                LastResults = matches.TakeLast(5).Select(s => s.BuildResults()).ToList(),
                HomeTeamRecord = new StatsRecord
                {
                    GamesWon = matches.Where(w => w.MatchData.FTResult == "H").Count(),
                    GamesLost = matches.Where(w => w.MatchData.FTResult == "A").Count(),
                    GamesDrawn = matches.Where(w => w.MatchData.FTResult == "D").Count()
                },
                ReverseFixture = this.urlService.FixtureStats(firstMatch.AwayTeamId, firstMatch.HomeTeamId, competitionId, seasonId),
                AllMatches = this.urlService.AllMatchesTeams(homeTeam, awayTeam, competitionId, seasonId)
            };

            fixtureStats.FirstResult.MatchData = this.urlService.MatchData(fixtureStats.FirstResult.MatchId);
            fixtureStats.LastResults.Select(s => { s.MatchData = this.urlService.MatchData(s.MatchId); return s; }).ToList();

            return fixtureStats;
        }
    }
}
