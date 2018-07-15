using System;
using Futbol.Common.Models.DataModels;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
    public class StatsMatch
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string BoxScore { get; set; }

        public DateTime MatchDate { get; set; }

        public string CompetitionSeason { get; set; }

        [JsonIgnore]
        public int MatchId { get; set; }

        public NavigationReferences _navigation { get; set; }
    }
}
