using System.Collections.Generic;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IEnglishSoccorDataRepository : IRepository
    {
        /// <summary>
        /// Parses the football bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Parsed football data from CSV</returns>
        List<Fixtures> ParseFootballBetData(string fileName);
    }
}
