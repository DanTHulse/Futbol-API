﻿using System;
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

        private readonly string FBUrl;

        private readonly string StatsUrl;

        public StatsService(IFutbolRepository futbolRepository, IConfigurationRoot configuration)
        {
            this.futbolRepository = futbolRepository;
            this.FBUrl = configuration.GetValue<string>("AppSettings:FootballApiUrl");
            this.StatsUrl = configuration.GetValue<string>("AppSettings:StatsApiUrl");

        }

        /// <summary>
        /// Retrieves the score stats.
        /// </summary>
        /// <param name="firstBoxScore">The first box score.</param>
        /// <param name="secondBoxScore">The second box score.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <param name="fullTime">if set to <c>true</c> [full time].</param>
        /// <returns></returns>
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
                AllMatches = new Uri($"{this.FBUrl}?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&competitionId={competitionId}&seasonId={seasonId}")
            };

            scoreStats.FirstMatch.MatchData = new Uri($"{this.FBUrl}?matchId={scoreStats.FirstMatch.MatchId}");
            scoreStats.LastMatch.MatchData = new Uri($"{this.FBUrl}?matchId={scoreStats.LastMatch.MatchId}");

            return scoreStats;
        }

        /// <summary>
        /// Retrieves the team stats.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="competitionId">The competition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
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
                Record = new StatsRecords
                {
                    GamesWon = wins.all.Count(),
                    GamesLost = losses.all.Count(),
                    GamesDrawn = draws.all.Count()
                },
                HomeRecord = new StatsRecords
                {
                    GamesWon = wins.home.Count(),
                    GamesLost = losses.home.Count(),
                    GamesDrawn = draws.home.Count()
                },
                AwayRecord = new StatsRecords
                {
                    GamesWon = wins.away.Count(),
                    GamesLost = losses.away.Count(),
                    GamesDrawn = draws.away.Count()
                },
                MostGamesPlayedAgainst = matches.CalculateMostPlayed(team.TeamId),
                MostWinsAgainst = wins.all.CalculateMostPlayed(team.TeamId),
                MostLossesAgainst = losses.all.CalculateMostPlayed(team.TeamId),
                MostDrawsAgainst = draws.all.CalculateMostPlayed(team.TeamId)
            };

            teamStats.BiggestWin.AllMatches = new Uri($"{this.FBUrl}?boxScoreFirst={teamStats.BiggestWin.Goals_1}&boxScoreSecond={teamStats.BiggestWin.Goals_2}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");
            teamStats.BiggestWin.FirstMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestWin.FirstMatch.MatchId}");
            teamStats.BiggestWin.LastMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestWin.LastMatch.MatchId}");

            teamStats.BiggestLoss.AllMatches = new Uri($"{this.FBUrl}?boxScoreFirst={teamStats.BiggestLoss.Goals_1}&boxScoreSecond={teamStats.BiggestLoss.Goals_2}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");
            teamStats.BiggestLoss.FirstMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestLoss.FirstMatch.MatchId}");
            teamStats.BiggestLoss.LastMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestLoss.LastMatch.MatchId}");

            teamStats.BiggestDraw.AllMatches = new Uri($"{this.FBUrl}?boxScoreFirst={teamStats.BiggestDraw.Goals_1}&boxScoreSecond={teamStats.BiggestDraw.Goals_2}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");
            teamStats.BiggestDraw.FirstMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestDraw.FirstMatch.MatchId}");
            teamStats.BiggestDraw.LastMatch.MatchData = new Uri($"{this.FBUrl}?matchId={teamStats.BiggestDraw.LastMatch.MatchId}");

            teamStats.MostGamesPlayedAgainst.Select(s => { s.AllMatches = new Uri($"{this.FBUrl}?teamId={s.Team_1}&teamId_2={s.Team_2}&competitionId={competitionId}&seasonId={seasonId}"); return s; }).ToList();
            teamStats.MostWinsAgainst.Select(s => { s.AllMatches = new Uri($"{this.FBUrl}?teamId={s.Team_1}&teamId_2={s.Team_2}&competitionId={competitionId}&seasonId={seasonId}"); return s; }).ToList();
            teamStats.MostLossesAgainst.Select(s => { s.AllMatches = new Uri($"{this.FBUrl}?teamId={s.Team_1}&teamId_2={s.Team_2}&competitionId={competitionId}&seasonId={seasonId}"); return s; }).ToList();
            teamStats.MostDrawsAgainst.Select(s => { s.AllMatches = new Uri($"{this.FBUrl}?teamId={s.Team_1}&teamId_2={s.Team_2}&competitionId={competitionId}&seasonId={seasonId}"); return s; }).ToList();

            return teamStats;
        }

        /// <summary>
        /// Retrieves the fixtures stats.
        /// </summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="competitionId">The conpetition identifier.</param>
        /// <param name="seasonId">The season identifier.</param>
        /// <returns></returns>
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
                HomeTeamRecord = new StatsRecords
                {
                    GamesWon = matches.Where(w => w.MatchData.FTResult == "H").Count(),
                    GamesLost = matches.Where(w => w.MatchData.FTResult == "A").Count(),
                    GamesDrawn = matches.Where(w => w.MatchData.FTResult == "D").Count()
                },
                ReverseFixture = new Uri($"{this.StatsUrl}/{firstMatch.AwayTeamId}/{firstMatch.HomeTeamId}?competitionId={competitionId}&seasonId={seasonId}"),
                FixtureMatches = new Uri($"{this.FBUrl}?homeTeamId={homeTeam}&awayTeamId={awayTeam}&competitionId={competitionId}&seasonId={seasonId}")
            };

            fixtureStats.FirstResult.MatchData = new Uri($"{this.FBUrl}?matchId={fixtureStats.FirstResult.MatchId}");
            fixtureStats.LastResults.Select(s => { s.MatchData = new Uri($"{this.FBUrl}?matchId={s.MatchId}"); return s; }).ToList();

            return fixtureStats;
        }
    }
}