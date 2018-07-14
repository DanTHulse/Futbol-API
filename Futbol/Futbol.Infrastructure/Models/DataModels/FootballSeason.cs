using System;
using System.Linq;

namespace Futbol.Common.Models.DataModels
{
    public class FootballSeason
    {
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public string SeasonPeriodDescription
            => $"Season takes place between " +
                        (this.SeasonPeriod.Contains("/") ? $"[Mid-{this.SeasonPeriod.Split('/').First()} to Mid-{this.SeasonPeriod.Split('/').Last()}]"
                                                         : $"[Early-{SeasonPeriod} to Late-{SeasonPeriod}]");

        public Uri AllMatches { get; set; }
    }
}
