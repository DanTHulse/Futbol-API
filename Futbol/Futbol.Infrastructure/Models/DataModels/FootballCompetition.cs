using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class FootballCompetition
    {
        [JsonIgnore]
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }

        public string TierLevel => Tier.HasValue ? $"Level {Tier.Value}" : $"Cup";

        [JsonIgnore]
        public int? Tier { get; set; }

        public NavigationReferences _navigation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<FootballCompetitionSeasons> Seasons { get; set; }
    }

    public class FootballCompetitionSeasons
    {
        [JsonIgnore]
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public int MatchCount { get; set; }

        public NavigationReferences _navigation { get; set; }
    }
}
