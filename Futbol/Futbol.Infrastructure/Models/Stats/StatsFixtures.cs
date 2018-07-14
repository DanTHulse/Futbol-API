using System;
using System.Collections.Generic;

namespace Futbol.Common.Models.Stats
{
    public class StatsFixtures
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public StatsMatch FirstResult { get; set; }

        public IEnumerable<StatsMatch> LastResults { get; set; }

        public StatsRecord HomeTeamRecord { get; set; }

        public Uri ReverseFixture { get; set; }

        public Uri AllMatches { get; set; }
    }
}