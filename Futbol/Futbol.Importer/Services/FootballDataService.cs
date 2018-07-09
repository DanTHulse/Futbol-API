using System;
using System.Linq;
using Futbol.Common.Models.Football;
using Futbol.Importer.DataModels.FootballData;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IFootballDataRepository footballDataRepository;

        private readonly IFutbolService futbolService;

        public FootballDataService(IFootballDataRepository footballDataRepository, IFutbolService futbolService)
        {
            this.footballDataRepository = footballDataRepository;
            this.futbolService = futbolService;
        }

        public void RetrieveCompetitionsBySeason(int seasonStartYear)
        {
            ConsoleLog.Start("Retrieving competitions");

            var competitions = this.footballDataRepository.RetriveCompetitionsBySeason(seasonStartYear);

            ConsoleLog.Information($"Competitions retrieved", $"{competitions.Count()}");

            if (competitions != null && competitions.Any())
            {
                foreach (var competition in competitions)
                {
                    ConsoleLog.Start($"Retrieving fxtures for {competition.Caption}");

                    var fixtureHeader = this.footballDataRepository.RetrieveFixturesForCompetition(competition.Id);

                    if (fixtureHeader.Fixtures != null && fixtureHeader.Fixtures.Any())
                    {
                        competition.Fixtures = fixtureHeader.Fixtures.Where(w => w.Status == "FINISHED" || string.IsNullOrEmpty(w.Status)).ToList();

                        ConsoleLog.Information($"Fixtures retrieved for {competition.Caption}", $"{competition.Fixtures.Count()}");

                        if (competition.Fixtures != null && competition.Fixtures.Any())
                        {
                            this.MapRecords(competition, seasonStartYear);
                        }
                    }
                    else
                    {
                        ConsoleLog.Error($"No fixtures found", $"{competition.Caption} - {seasonStartYear}");
                    }
                }
            }
        }

        private void MapRecords(Competitions competition, int seasonStartYear)
        {
            var seasonCode = (seasonStartYear % 100).ToString().PadLeft(2, '0');

            var seasonId = this.futbolService.RetrieveSeasonByStartYear(Int32.Parse(seasonCode)).SeasonId;
            var competitionId = this.futbolService.RetrieveCompetitionByName(competition.Caption).CompetitionId;

            var allTeams = competition.Fixtures.Select(s => s.HomeTeamName).ToList();
            allTeams.AddRange(competition.Fixtures.Select(a => a.AwayTeamName).ToList());

            var distinctTeams = allTeams.Distinct();

            var teamRecords = distinctTeams.Select(s => this.futbolService.RetrieveTeamByName(s)).ToList();

            var matches = competition.Fixtures.Select(s => new Match
            {
                HomeTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.HomeTeamName).TeamId,
                AwayTeamId = teamRecords.FirstOrDefault(f => f.TeamName == s.AwayTeamName).TeamId,
                MatchDate = s.Date,
                SeasonId = seasonId,
                CompetitionId = competitionId,
                MatchUid = this.BuildMatchUid(s.HomeTeamName, s.AwayTeamName, s.Date),
                MatchData = new MatchData
                {
                    FTHomeGoals = s.Result.GoalsHomeTeam,
                    FTAwayGoals = s.Result.GoalsAwayTeam,
                    FTResult = this.CalculateResult(s.Result.GoalsHomeTeam, s.Result.GoalsAwayTeam),
                    HTHomeGoals = s.Result.HalfTime?.GoalsHomeTeam,
                    HTAwayGoals = s.Result.HalfTime?.GoalsAwayTeam,
                    HTResult = this.CalculateResult(s.Result.HalfTime?.GoalsHomeTeam, s.Result.HalfTime?.GoalsAwayTeam)
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
