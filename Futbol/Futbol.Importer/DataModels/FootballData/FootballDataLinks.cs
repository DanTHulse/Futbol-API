using Newtonsoft.Json;

namespace Futbol.Importer.DataModels.FootballData
{
    public class FootballDataLinks
    {
        [JsonProperty("self")]
        public Link Self { get; set; }

        [JsonProperty("teams")]
        public Link Teams { get; set; }

        [JsonProperty("fixtures")]
        public Link Fixtures { get; set; }

        [JsonProperty("leagueTable")]
        public Link LeagueTable { get; set; }

        [JsonProperty("competition")]
        public Link Competition { get; set; }

        [JsonProperty("homeTeam")]
        public Link HomeTeam { get; set; }

        [JsonProperty("awayTeam")]
        public Link AwayTeam { get; set; }
    }

    public class Link
    {
        [JsonProperty("href")]
        public string HRef { get; set; }
    }
}
