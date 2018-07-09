using System;

namespace Futbol.API.DataModels
{
    public class FootballSeason
    {
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public Uri AllMatches { get; set; }
    }
}
