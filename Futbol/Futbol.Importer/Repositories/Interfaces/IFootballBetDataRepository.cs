using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballBetData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFootballBetDataRepository : IRepository
    {
        /// <summary>
        /// Parses the football bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Parsed football data from CSV</returns>
        List<Fixture> ParseFootballBetData(string fileName);
    }
}
