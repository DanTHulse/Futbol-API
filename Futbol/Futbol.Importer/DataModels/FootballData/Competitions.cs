using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.Importer.DataModels.FootballData
{
    public class Competitions
    {
        [JsonProperty("_links")]
        public FootballDataLinks Links { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        public List<Fixtures> Fixtures { get; set; }
    }
}
