using System.Collections.Generic;
using Futbol.Common.Models.DataModels;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
    public class StatsTeam
    {
        public string TeamName { get; set; }
        
        public StatsScores BiggestWin { get; set; }

        public StatsScores BiggestLoss { get; set; }

        public StatsScores BiggestDraw { get; set; }

        public StatsRecord Record { get; set; }

        public StatsRecord HomeRecord { get; set; }

        public StatsRecord AwayRecord { get; set; }

        public IEnumerable<StatsTeamMatchups> MostGamesPlayedAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostWinsAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostLossesAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostDrawsAgainst { get; set; }
    }

    public class StatsTeamMatchups
    {
        public string TeamName { get; set; }

        public int Count { get; set; }

        public NavigationReferences _navigation { get; set; }

        [JsonIgnore]
        public int Team_1 { get; set; }

        [JsonIgnore]
        public int Team_2 { get; set; }
    }

    public class StatsTeamMatchups_Grouping
    {
        public string FirstTeam { get; set; }

        public int FirstTeamId { get; set; }

        public string SecondTeam { get; set; }

        public int SecondTeamId { get; set; }
    }
}
