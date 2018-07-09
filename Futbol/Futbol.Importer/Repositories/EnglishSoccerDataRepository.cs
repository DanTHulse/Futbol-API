using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Futbol.Importer.DataModels.EngSoccorData;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;

namespace Futbol.Importer.Repositories
{
    public class EnglishSoccerDataRepository : IEnglishSoccerDataRepository
    {
        private readonly Configuration config;

        public EnglishSoccerDataRepository(Configuration config)
        {
            this.config = config;
        }

        public List<Fixtures> ParseFootballBetData(string fileName)
        {
            using (TextReader csvText = File.OpenText(fileName))
            {
                CsvReader csv = new CsvReader(csvText);
                csv.Configuration.RegisterClassMap<EngSoccorDataMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.IgnoreBlankLines = true;

                var records = csv.GetRecords<Fixtures>().ToList();

                return records.ToList();
            }
        }
    }
}
