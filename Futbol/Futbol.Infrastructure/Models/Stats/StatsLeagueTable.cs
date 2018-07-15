using System.Collections.Generic;
using Futbol.Common.Models.DataModels;

namespace Futbol.Common.Models.Stats
{
    public class StatsLeagueTable
    {
        public string CompetitionName { get; set; }

        public int? Tier { get; set; }

        public string SeasonPeriod { get; set; }

        public NavigationReferences _navigation { get; set; }

        public IEnumerable<StatsCompetitionSeasonTeams> Teams { get; set; }
    }
}
