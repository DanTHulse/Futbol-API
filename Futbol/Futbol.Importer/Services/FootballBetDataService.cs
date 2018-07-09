using System;
using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Models.Football;
using Futbol.Importer.DataModels.FootballBetData;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class FootballBetDataService : IFootballBetDataService
    {
        private readonly IFootballBetDataRepository footballBetDataRepository;

        private readonly IFutbolService futbolService;

        public FootballBetDataService(IFootballBetDataRepository footballBetDataRepository, IFutbolService futbolService)
        {
            this.footballBetDataRepository = footballBetDataRepository;
            this.futbolService = futbolService;
        }

        public void ImportBetData(string fileName, string competitionName, int seasonStart)
        {
            ConsoleLog.Start($"Parsing fixtures: {competitionName} - {seasonStart}");

            var fixtures = this.footballBetDataRepository.ParseFootballBetData(fileName);

            if (fixtures != null && fixtures.Any())
            {
                ConsoleLog.Information("Fixtures found:", $"{fixtures.Count()}");

                fixtures.Select(s => { s.Division = competitionName; s.Season = seasonStart.ToString(); return s; });

                this.MapRecords(fixtures, competitionName, seasonStart);
            }
            else
            {
                ConsoleLog.Error($"No fixtures found for file: {fileName}", $"{competitionName} - {seasonStart}");
            }
        }
        private void MapRecords(List<Fixture> fixtures, string competitionName, int seasonStart)
        {
            var seasonId = this.futbolService.RetrieveSeasonByStartYear(seasonStart).SeasonId;
            var competitionId = this.futbolService.RetrieveCompetitionByName(competitionName).CompetitionId;

            var allTeams = fixtures.Select(s => s.HomeTeam).ToList();
            allTeams.AddRange(fixtures.Select(a => a.AwayTeam).ToList());

            var distinctTeams = allTeams.Distinct();

            var teamRecords = distinctTeams.Select(s => this.futbolService.RetrieveTeamByName(s)).ToList();

            var matches = fixtures.Select(s => new Match
            {
                HomeTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.HomeTeam || f.AlternateTeamName == s.HomeTeam).TeamId,
                AwayTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.AwayTeam || f.AlternateTeamName == s.AwayTeam).TeamId,
                MatchDate = s.MatchDate.Value,
                SeasonId = seasonId,
                CompetitionId = competitionId,
                MatchUid = this.BuildMatchUid(s.HomeTeam, s.AwayTeam, s.MatchDate.Value),
                MatchData = new MatchData
                {
                    FTHomeGoals = s.FullTimeHomeGoals,
                    FTAwayGoals = s.FullTimeAwayGoals,
                    FTResult = this.CalculateResult(s.FullTimeHomeGoals, s.FullTimeAwayGoals),
                    HTHomeGoals = s.HalfTimeHomeGoals,
                    HTAwayGoals = s.HalfTimeAwayGoals,
                    HTResult = this.CalculateResult(s.HalfTimeHomeGoals, s.HalfTimeAwayGoals),
                    HomeShots = s.HomeShots,
                    AwayShots = s.AwayShots,
                    HomeShotsOnTarget = s.HomeShotsOnTarget,
                    AwayShotsOnTarget = s.AwayShotsOnTarget
                }
            }).ToList();

            this.futbolService.InsertMatches(matches);
        }

        private string CalculateResult(int? homeGoals, int? awayGoals)
        {
            if (homeGoals == null)
                return "U";
            else if (homeGoals > awayGoals)
                return "H";
            else if (homeGoals < awayGoals)
                return "A";
            else
                return "D";
        }

        private string BuildMatchUid(string homeTeam, string awayTeam, DateTime matchDate)
        {
            var homeTeamTag = homeTeam.Replace(" ", "").Replace(".", "").Left(5).ToUpper();
            var awayTeamTag = awayTeam.Replace(" ", "").Replace(".", "").Left(5).ToUpper();

            return $"{homeTeamTag}{awayTeamTag}{matchDate.ToString("yyyyMMdd")}";
        }
    }
}
