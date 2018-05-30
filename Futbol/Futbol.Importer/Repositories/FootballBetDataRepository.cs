using System.IO;
using System.Linq;
using CsvHelper;
using Futbol.Importer.DataModels.FootballBetData;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;

namespace Futbol.Importer.Repositories
{
    public class FootballBetDataRepository : IFootballBetDataRepository
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly Configuration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballBetDataRepository"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public FootballBetDataRepository(Configuration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Parses the football bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Parsed football data from CSV</returns>
        public object ParseFootballBetData(string fileName, string competitionName)
        {
            using (TextReader csvText = File.OpenText(fileName))
            {
                CsvReader csv = new CsvReader(csvText);
                csv.Configuration.RegisterClassMap<FootballBetMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;

                var records = csv.GetRecords<Fixture>().ToList();
            }
               
            return null;
        }
    }
}
