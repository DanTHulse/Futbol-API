using System;
using System.Collections.Generic;
using Futbol.Common.Models.Football;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class FutbolService : IFutbolService
    {
        private readonly IFutbolRepository futbolRepository;

        public FutbolService(IFutbolRepository futbolRepository)
        {
            this.futbolRepository = futbolRepository;
        }

        public Team RetrieveTeamByName(string name)
        {
            var team = this.futbolRepository.RetrieveTeamByName(name);

            if (team == null)
            {
                ConsoleLog.Information($"Team not found:", $"{name}");
                Console.WriteLine();
                Console.WriteLine($"Is this an alternate spelling? [Y/n]");

                var answer = Console.ReadLine();

                if (answer == "n")
                {
                    ConsoleLog.Information($"New Team added to database", $"{name}");
                    Console.WriteLine();

                    this.futbolRepository.Add(new Team { TeamName = name });
                    team = this.futbolRepository.RetrieveTeamByName(name);
                }
                else
                {
                    Console.WriteLine($"Enter Id for team");
                    var teamId = ConsoleEx.ReadNumber();
                    team = this.futbolRepository.RetrieveTeamById(teamId);

                    team.AlternateTeamName = name;
                    this.futbolRepository.UpdateTeam(team);
                }
            }

            return team;
        }

        public Season RetrieveSeasonByStartYear(int seasonStartYear)
        {
            var season = this.futbolRepository.RetrieveSeasonByStartYear(seasonStartYear);

            return season;
        }

        public Competition RetrieveCompetitionByName(string name)
        {
            var competition = this.futbolRepository.RetrieveCompetitionByName(name);

            if (competition == null)
            {
                ConsoleLog.Information($"New Competition added to database", $"{name}");

                this.futbolRepository.Add(new Competition { CompetitionName = name });
                competition = this.futbolRepository.RetrieveCompetitionByName(name);
            }

            return competition;
        }

        public void InsertMatches(List<Match> matches)
        {
            this.futbolRepository.InsertMatches(matches);
        }
    }
}
