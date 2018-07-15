using System.Collections.Generic;
using Futbol.Common.Models.DataModels;

namespace Futbol.Common.Models.Stats
{
    public class StatsFixtures
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public StatsMatch FirstResult { get; set; }

        public IEnumerable<StatsMatch> LastResults { get; set; }

        public StatsRecord HomeTeamRecord { get; set; }

        public NavigationReferences _navigation { get; set; }
    }
}