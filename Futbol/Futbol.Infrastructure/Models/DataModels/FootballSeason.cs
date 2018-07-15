using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class FootballSeason
    {
        [JsonIgnore]
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public string SeasonPeriodDescription
            => $"Season takes place between " +
                        (this.SeasonPeriod.Contains("/") ? $"[Mid-{this.SeasonPeriod.Split('/').First()} to Mid-{this.SeasonPeriod.Split('/').Last()}]"
                                                         : $"[Early-{SeasonPeriod} to Late-{SeasonPeriod}]");

        public NavigationReferences _navigation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<FootballSeasonCompetition> Competitions { get; set; }
    }

    public class FootballSeasonCompetition
    {
        [JsonIgnore]
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }

        public string TierLevel => Tier.HasValue ? $"Level {Tier.Value}" : $"Cup";

        [JsonIgnore]
        public int? Tier { get; set; }

        public int MatchCount { get; set; }

        public NavigationReferences _navigation { get; set; }
    }
}
