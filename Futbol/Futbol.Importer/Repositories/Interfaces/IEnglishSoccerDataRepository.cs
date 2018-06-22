using System.Collections.Generic;
using Futbol.Importer.DataModels.EngSoccorData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IEnglishSoccerDataRepository : IRepository
    {
        /// <summary>
        /// Parses the football bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Parsed football data from CSV</returns>
        List<Fixtures> ParseFootballBetData(string fileName);
    }
}
