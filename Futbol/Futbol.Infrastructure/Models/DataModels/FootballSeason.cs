using System;
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

        [JsonProperty("SeasonDetails")]
        public Uri Reference { get; set; }

        public Uri AllMatches { get; set; }
    }
}
