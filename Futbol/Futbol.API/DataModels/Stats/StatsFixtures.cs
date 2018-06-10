using System;
using System.Collections.Generic;

namespace Futbol.API.DataModels.Stats
{
    public class StatsFixtures
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public StatsResults FirstResult { get; set; }

        public IEnumerable<StatsResults> LastResults { get; set; }

        public StatsRecords HomeTeamRecord { get; set; }

        public Uri ReverseFixture { get; set; }

        public Uri FixtureMatches { get; set; }
    }
}
