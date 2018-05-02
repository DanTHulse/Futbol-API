using System.Collections.Generic;
using Futbol.Common.Models.Football;

namespace Futbol.Importer.Services.Interfaces
{
    public interface IFutbolService : IService
    {
        /// <summary>
        /// Retrieves the name of the team by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Team RetrieveTeamByName(string name);

        /// <summary>
        /// Retrieves the season by start year.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        /// <returns></returns>
        Season RetrieveSeasonByStartYear(int seasonStartYear);

        /// <summary>
        /// Retrieves the name of the competition by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Competition RetrieveCompetitionByName(string name);

        /// <summary>
        /// Inserts the matches.
        /// </summary>
        /// <param name="matches">The matches.</param>
        void InsertMatches(List<Match> matches);
    }
}
