using System;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
    public class ScorigamiScores
    {
        public string BoxScore => $"{this.Score_1}-{this.Score_2}";

        [JsonIgnore]
        public int? Score_1 { get; set; }

        [JsonIgnore]
        public int? Score_2 { get; set; }

        public int Count { get; set; }

        public Uri ScoreStats { get; set; }
    }
}
