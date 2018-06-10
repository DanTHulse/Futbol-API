using System;

namespace Futbol.API.DataModels.Stats
{
    public class StatsScores
    {
        public string BoxScore { get; set; }

        public StatsResults FirstMatch { get; set; }

        public StatsResults LastMatch { get; set; }

        public int Count { get; set; }

        public Uri AllMatches { get; set; }
    }
}
