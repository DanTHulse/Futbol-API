using System.Collections.Generic;
using Futbol.Common.Models.Football;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;

namespace Futbol.Importer.Services
{
    public class FutbolService : IFutbolService
    {
        /// <summary>
        /// The futbol repository
        /// </summary>
        private readonly IFutbolRepository futbolRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FutbolService"/> class.
        /// </summary>
        /// <param name="futbolRepository">The futbol repository.</param>
        public FutbolService(IFutbolRepository futbolRepository)
        {
            this.futbolRepository = futbolRepository;
        }

        /// <summary>
        /// Retrieves the name of the team by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Team RetrieveTeamByName(string name)
        {
            var team = this.futbolRepository.RetrieveTeamByName(name);

            if (team == null)
            {
                ConsoleLog.Information($"New Team added to database", $"{name}");

                this.futbolRepository.Add(new Team { TeamName = name });
                team = this.futbolRepository.RetrieveTeamByName(name);
            }

            return team;
        }

        /// <summary>
        /// Retrieves the season by start year.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        /// <returns></returns>
        public Season RetrieveSeasonByStartYear(int seasonStartYear)
        {
            var season = this.futbolRepository.RetrieveSeasonByStartYear(seasonStartYear);

            return season;
        }

        /// <summary>
        /// Retrieves the name of the competition by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserts the matches.
        /// </summary>
        /// <param name="matches">The matches.</param>
        public void InsertMatches(List<Match> matches)
        {
            this.futbolRepository.InsertMatches(matches);
        }
    }
}
