using System.Collections.Generic;
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
        private readonly Configuration config;

        public FootballBetDataRepository(Configuration config)
        {
            this.config = config;
        }

        public List<Fixture> ParseFootballBetData(string fileName)
        {
            using (TextReader csvText = File.OpenText(fileName))
            {
                CsvReader csv = new CsvReader(csvText);
                csv.Configuration.RegisterClassMap<FootballBetMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.IgnoreBlankLines = true;

                var records = csv.GetRecords<Fixture>().ToList();

                return records.Where(w => !string.IsNullOrEmpty(w.Division)).ToList();
            }
        }
    }
}
