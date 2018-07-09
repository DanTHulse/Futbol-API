using System;
using Newtonsoft.Json;

namespace Futbol.API.DataModels.Stats
{
    /// <summary>
    /// The stats for a chosen scoreline
    /// </summary>
    public class StatsScores
    {
        public string BoxScore { get; set; }

        public StatsMatch FirstMatch { get; set; }

        public StatsMatch LastMatch { get; set; }

        public int Count { get; set; }

        [JsonIgnore]
        public int Goals_1 { get; set; }

        [JsonIgnore]
        public int Goals_2 { get; set; }

        [JsonIgnore]
        public Uri AllMatches { get; set; }
    }
}
