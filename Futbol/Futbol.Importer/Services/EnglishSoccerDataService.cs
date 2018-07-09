using System;
using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Models.Football;
using Futbol.Importer.DataModels.EngSoccorData;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class EnglishSoccerDataService : IEnglishSoccerDataService
    {
        private readonly IFutbolService futbolService;

        private readonly IEnglishSoccerDataRepository englishSoccerDataRepository;

        public EnglishSoccerDataService(IFutbolService futbolService, IEnglishSoccerDataRepository englishSoccerDataRepository)
        {
            this.englishSoccerDataRepository = englishSoccerDataRepository;
            this.futbolService = futbolService;
        }

        public void ImportData(string fileName)
        {
            ConsoleLog.Start($"Parsing fixtures: ");

            var fixtures = this.englishSoccerDataRepository.ParseFootballBetData(fileName);

            if (fixtures != null && fixtures.Any())
            {
                ConsoleLog.Information("Fixtures found:", $"{fixtures.Count()}");

                this.MapRecords(fixtures);
            }
            else
            {
                ConsoleLog.Error($"No fixtures found for file: {fileName}", $"");
            }
        }

        private void MapRecords(List<Fixtures> fixtures)
        {
            var allSeasons = fixtures.Select(s => s.Season).Distinct();
            var allDivisions = fixtures.Select(s => s.Division).Distinct();

            var seasonIds = allSeasons.Select(s => this.futbolService.RetrieveSeasonByStartYear(s));
            var competitionIds = allDivisions.Select(s => this.futbolService.RetrieveCompetitionByName(s));

            var allTeams = fixtures.Select(s => s.HomeTeam).ToList();
            allTeams.AddRange(fixtures.Select(a => a.AwayTeam).ToList());

            var distinctTeams = allTeams.Distinct();

            var teamRecords = distinctTeams.Select(s => this.futbolService.RetrieveTeamByName(s)).ToList();

            Console.WriteLine();
            Console.WriteLine("Please enter the offset value: ");
            var offSet = ConsoleEx.ReadNumber();

            for (int x = offSet; x < fixtures.Count(); x += 500)
            {
                ConsoleLog.Start($"Parsing matches from", $"Offset: {x}");

                var matches = fixtures.Skip(x).Take(500).Select(s => new Match
                {
                    HomeTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.HomeTeam || f.AlternateTeamName == s.HomeTeam).TeamId,
                    AwayTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.AwayTeam || f.AlternateTeamName == s.AwayTeam).TeamId,
                    MatchDate = s.Date.Value,
                    SeasonId = seasonIds.FirstOrDefault(f => f.SeasonPeriod.StartsWith(s.Season.ToString())).SeasonId,
                    CompetitionId = competitionIds.FirstOrDefault(f => f.CompetitionName == s.Division).CompetitionId,
                    MatchUid = this.BuildMatchUid(teamRecords.FirstOrDefault(f => f.TeamName == s.HomeTeam || f.AlternateTeamName == s.HomeTeam).TeamName,
                                              teamRecords.FirstOrDefault(f => f.TeamName == s.AwayTeam || f.AlternateTeamName == s.AwayTeam).TeamName,
                                              s.Date.Value),
                    MatchData = new MatchData
                    {
                        FTHomeGoals = s.FTHomeGoals,
                        FTAwayGoals = s.FTAwayGoals,
                        FTResult = this.CalculateResult(s.FTHomeGoals, s.FTAwayGoals)
                    }
                }).ToList();

                ConsoleLog.Start($"Inserting matches from", $"Offset: {x}");

                this.futbolService.InsertMatches(matches);

                ConsoleLog.Information($"Finishing inserting matches from", $"Offset: {x} Remaining = {fixtures.Count() - (x + 500)}");
            }
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
            var homeTeamTag = homeTeam.Replace(" ", "").Replace(".", "");
            var awayTeamTag = awayTeam.Replace(" ", "").Replace(".", "");

            return $"{homeTeamTag}{awayTeamTag}{matchDate.ToString("yyyyMMdd")}";
        }
    }
}
