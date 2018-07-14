using System;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
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
