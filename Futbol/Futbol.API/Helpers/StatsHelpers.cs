﻿using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Models.Enumerations;
using Futbol.Common.Models.Football;
using Futbol.Common.Models.Stats;

namespace Futbol.API.Helpers
{
    public static class StatsHelpers
    {
        public static StatsMatch BuildResults(this Match match)
        {
            return new StatsMatch
            {
                BoxScore = $"{match.MatchData.FTHomeGoals}-{match.MatchData.FTAwayGoals}",
                HomeTeam = $"{match.HomeTeam.TeamName}",
                AwayTeam = $"{match.AwayTeam.TeamName}",
                MatchDate = match.MatchDate,
                CompetitionSeason = $"{match.Competition.CompetitionName} {match.Season.SeasonPeriod}",
                MatchId = match.MatchId
            };
        }

        public static (IEnumerable<Match> all, IEnumerable<Match> home, IEnumerable<Match> away) SplitMatchesByResult(this IEnumerable<Match> matches, int teamId, Result result)
        {
            var allMatches = new List<Match>();
            var homeMatches = new List<Match>();
            var awayMatches = new List<Match>();

            switch (result)
            {
                case Result.Win:
                    homeMatches.AddRange(matches.Where(w => w.HomeTeamId == teamId && w.MatchData.FTResult == "H"));
                    awayMatches.AddRange(matches.Where(w => w.AwayTeamId == teamId && w.MatchData.FTResult == "A"));
                    break;
                case Result.Loss:
                    homeMatches.AddRange(matches.Where(w => w.HomeTeamId == teamId && w.MatchData.FTResult == "A"));
                    awayMatches.AddRange(matches.Where(w => w.AwayTeamId == teamId && w.MatchData.FTResult == "H"));
                    break;
                case Result.Draw:
                    homeMatches.AddRange(matches.Where(w => w.HomeTeamId == teamId && w.MatchData.FTResult == "D"));
                    awayMatches.AddRange(matches.Where(w => w.AwayTeamId == teamId && w.MatchData.FTResult == "D"));
                    break;
            }

            allMatches.AddRange(homeMatches);
            allMatches.AddRange(awayMatches);

            return (all: allMatches, home: homeMatches, away: awayMatches);
        }

        public static StatsScores CalculateBiggestResult(this IEnumerable<Match> matches)
        {
            if (matches == null || !matches.Any())
            {
                return null;
            }

            var orderedMatches = matches.OrderByDescending(o => o.MatchData.FTGoals_1);

            var filteredMatches = orderedMatches.Where(w => w.MatchData.FTGoals_1 == orderedMatches.First().MatchData.FTGoals_1
                                                    && w.MatchData.FTGoals_2 == orderedMatches.First().MatchData.FTGoals_2)
                                                .OrderBy(o => o.MatchDate);

            return new StatsScores
            {
                BoxScore = $"{filteredMatches.First().MatchData.FTGoals_1.Value}-{filteredMatches.First().MatchData.FTGoals_2.Value}",
                FirstMatch = filteredMatches.First().BuildResults(),
                LastMatch = filteredMatches.Last().BuildResults(),
                Count = filteredMatches.Count(),
                Goals_1 = filteredMatches.First().MatchData.FTHomeGoals.Value,
                Goals_2 = filteredMatches.First().MatchData.FTAwayGoals.Value,
            };
        }

        public static IEnumerable<StatsTeamMatchups> CalculateMostPlayed(this IEnumerable<Match> matches, int teamId)
        {
            if (matches == null || !matches.Any())
            {
                return null;
            }

            var matchups = matches.Where(w => w.HomeTeamId == teamId).Select(s => new StatsTeamMatchups_Grouping
            {
                FirstTeam = s.HomeTeam.TeamName,
                FirstTeamId = teamId,
                SecondTeam = s.AwayTeam.TeamName,
                SecondTeamId = s.AwayTeamId
            }).ToList();

            matchups.AddRange(matches.Where(w => w.AwayTeamId == teamId).Select(s => new StatsTeamMatchups_Grouping
            {
                FirstTeam = s.AwayTeam.TeamName,
                FirstTeamId = teamId,
                SecondTeam = s.HomeTeam.TeamName,
                SecondTeamId = s.HomeTeamId
            }).ToList());

            var groupedMatches = matchups
                .GroupBy(i => i.SecondTeamId)
                .Select(grp => new { Item = grp.Key, Count = grp.Count() })
                .OrderByDescending(grp => grp.Count)
                .Take(3)
                .ToList();

            List<StatsTeamMatchups> stats = new List<StatsTeamMatchups>();

            foreach (var group in groupedMatches)
            {
                var secondTeam = matchups.First(f => f.SecondTeamId == group.Item);

                var stat = new StatsTeamMatchups
                {
                    TeamName = $"{secondTeam.SecondTeam}",
                    Count = group.Count,
                    Team_1 = teamId,
                    Team_2 = secondTeam.SecondTeamId
                };

                stats.Add(stat);
            }

            return stats;
        }

        public static IEnumerable<StatsCompetitionSeasonTeams> CalculateTeamRecords(this IEnumerable<Match> matches, IEnumerable<(int teamId, string teamName)> teams)
        {
            var teamRecords = new List<StatsCompetitionSeasonTeams>();

            foreach (var team in teams)
            {
                var goalsScored = (matches.Where(w => ((w.MatchData.FTResult == "H" || w.MatchData.FTResult == "D") && w.HomeTeamId == team.teamId)
                            || (w.MatchData.FTResult == "A" && w.AwayTeamId == team.teamId)).Sum(s => s.MatchData.FTGoals_1.Value))
                                + (matches.Where(w => ((w.MatchData.FTResult == "H" || w.MatchData.FTResult == "D") && w.AwayTeamId == team.teamId)
                            || (w.MatchData.FTResult == "A" && w.HomeTeamId == team.teamId)).Sum(s => s.MatchData.FTGoals_2.Value));

                var goalsConceded = (matches.Where(w => ((w.MatchData.FTResult == "H" || w.MatchData.FTResult == "D") && w.HomeTeamId == team.teamId)
                            || (w.MatchData.FTResult == "A" && w.AwayTeamId == team.teamId)).Sum(s => s.MatchData.FTGoals_2.Value))
                                  + (matches.Where(w => ((w.MatchData.FTResult == "H" || w.MatchData.FTResult == "D") && w.AwayTeamId == team.teamId)
                            || (w.MatchData.FTResult == "A" && w.HomeTeamId == team.teamId)).Sum(s => s.MatchData.FTGoals_1.Value));

                teamRecords.Add(new StatsCompetitionSeasonTeams
                {
                    TeamName = team.teamName,
                    TeamId = team.teamId,
                    GoalsScored = goalsScored,
                    GoalsConceded = goalsConceded,
                    GamesWon = matches.SplitMatchesByResult(team.teamId, Result.Win).all?.Count() ?? 0,
                    GamesLost = matches.SplitMatchesByResult(team.teamId, Result.Loss).all?.Count() ?? 0,
                    GamesDrawn = matches.SplitMatchesByResult(team.teamId, Result.Draw).all?.Count() ?? 0
                });
            }

            return teamRecords;
        }
    }
}
