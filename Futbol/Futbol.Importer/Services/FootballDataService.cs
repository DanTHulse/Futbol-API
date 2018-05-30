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
        /// <summary>
        /// The football data repository
        /// </summary>
        private readonly IFootballDataRepository footballDataRepository;

        /// <summary>
        /// The futbol service
        /// </summary>
        private readonly IFutbolService futbolService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballDataService"/> class.
        /// </summary>
        /// <param name="footballDataRepository">The football data repository.</param>
        /// <param name="futbolService">The futbol service.</param>
        public FootballDataService(IFootballDataRepository footballDataRepository, IFutbolService futbolService)
        {
            this.footballDataRepository = footballDataRepository;
            this.futbolService = futbolService;
        }

        /// <summary>
        /// Retrieves the competitions by season.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
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

        /// <summary>
        /// Maps the records.
        /// </summary>
        /// <param name="competition">The competition.</param>
        /// <param name="seasonStartYear">The season start year.</param>
        private void MapRecords(Competitions competition, int seasonStartYear)
        {
            var seasonId = this.futbolService.RetrieveSeasonByStartYear(seasonStartYear).SeasonId;
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

        /// <summary>
        /// Calculates the result.
        /// </summary>
        /// <param name="homeGoals">The home goals.</param>
        /// <param name="awayGoals">The away goals.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Builds the match uid.
        /// </summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="matchDate">The match date.</param>
        /// <returns></returns>
        private string BuildMatchUid(string homeTeam, string awayTeam, DateTime matchDate)
        {
            var homeTeamTag = homeTeam.Replace(" ", "").Left(3).ToUpper();
            var awayTeamTag = awayTeam.Replace(" ", "").Left(3).ToUpper();

            return $"{homeTeamTag}{awayTeamTag}{matchDate.ToString("yyyyMMdd")}";
        }
    }
}
