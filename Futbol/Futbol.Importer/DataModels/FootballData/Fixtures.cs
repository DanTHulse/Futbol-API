using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.Importer.DataModels.FootballData
{
    public class FixturesHeader
    {
        [JsonProperty("_links")]
        public FootballDataLinks Links { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("fixtures")]
        public List<Fixtures> Fixtures { get; set; }
    }

    public class Fixtures
    {
        [JsonProperty("_links")]
        public FootballDataLinks Links { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("matchDay")]
        public int MatchDay { get; set; }

        [JsonProperty("homeTeamName")]
        public string HomeTeamName { get; set; }

        [JsonProperty("awayTeamName")]
        public string AwayTeamName { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public class Result
    {
        [JsonProperty("goalsHomeTeam")]
        public int GoalsHomeTeam { get; set; }

        [JsonProperty("goalsAwayTeam")]
        public int GoalsAwayTeam { get; set; }

        [JsonProperty("halfTime")]
        public HalfTime HalfTime { get; set; }
    }

    public class HalfTime
    {
        [JsonProperty("goalsHomeTeam")]
        public int? GoalsHomeTeam { get; set; }

        [JsonProperty("goalsAwayTeam")]
        public int? GoalsAwayTeam { get; set; }
    }
}
