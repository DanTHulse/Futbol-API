using System.Collections.Generic;
using Futbol.Common.Models.Football;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFutbolRepository : IRepository
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
        /// Adds the specified record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record">The record.</param>
        void Add<T>(T record) where T : class;

        /// <summary>
        /// Inserts the matches.
        /// </summary>
        /// <param name="records">The records.</param>
        void InsertMatches(List<Match> records);
    }
}
