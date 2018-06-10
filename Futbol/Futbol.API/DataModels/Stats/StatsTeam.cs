using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.API.DataModels.Stats
{
    public class StatsTeam
    {
        public string TeamName { get; set; }
        
        public StatsTeamScores BiggestWin { get; set; }

        public StatsTeamScores BiggestLoss { get; set; }

        public StatsTeamScores BiggestDraw { get; set; }

        public StatsRecords Record { get; set; }

        public StatsRecords HomeRecord { get; set; }

        public StatsRecords AwayRecord { get; set; }

        public IEnumerable<StatsTeamMatchups> MostGamesPlayedAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostWinsAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostLossesAgainst { get; set; }

        public IEnumerable<StatsTeamMatchups> MostDrawsAgainst { get; set; }
    }

    public class StatsTeamScores
    {
        public StatsResults FirstMatch { get; set; }

        public StatsResults LastMatch { get; set; }

        public int Count { get; set; }

        [JsonIgnore]
        public int Goals_1 { get; set; }

        [JsonIgnore]
        public int Goals_2 { get; set; }

        public Uri AllMatches { get; set; }
    }

    public class StatsTeamMatchups
    {
        public string TeamName { get; set; }

        public int Count { get; set; }

        public Uri AllMatches { get; set; }

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
