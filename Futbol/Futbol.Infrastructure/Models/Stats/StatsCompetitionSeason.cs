using System;
using Futbol.Common.Models.DataModels;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
    public class StatsCompetitionSeason
    {
        public int TotalGamesPlayed { get; set; }

        public int TotalGoalsScored { get; set; }

        public decimal AverageGoalsPerGame => Math.Truncate(((decimal)this.TotalGoalsScored / (decimal)this.TotalGamesPlayed) * 10m) / 10m;

        public string MostGoalsScored => $"{this.MostGoalsScoredAmount} - {this.MostGoalsScoredTeam}";

        public string LeastGoalsScored => $"{this.LeastGoalsScoredAmount} - {this.LeastGoalsScoredTeam}";

        public string MostGoalsConceded => $"{this.MostGoalsConcededAmount} - {this.MostGoalsConcededTeam}";

        public string LeastGoalsConceded => $"{this.LeastGoalsConcededAmount} - {this.LeastGoalsConcededTeam}";

        public string MostGamesWon => $"{this.MostGamesWonAmount} - {this.MostGamesWonTeam}";

        public string MostGamesLost => $"{this.MostGamesLostAmount} - {this.MostGamesLostTeam}";

        public string MostGamesDrawn => $"{this.MostGamesDrawnAmount} - {this.MostGamesDrawnTeam}";

        public NavigationReferences _navigation { get; set; }

        public StatsScores BiggestWin { get; set; }

        public StatsScores BiggestDraw { get; set; }

        #region IgnoredProperties

        [JsonIgnore]
        public int MostGoalsScoredAmount { get; set; }

        [JsonIgnore]
        public string MostGoalsScoredTeam { get; set; }

        [JsonIgnore]
        public int LeastGoalsScoredAmount { get; set; }

        [JsonIgnore]
        public string LeastGoalsScoredTeam { get; set; }

        [JsonIgnore]
        public int MostGoalsConcededAmount { get; set; }

        [JsonIgnore]
        public string MostGoalsConcededTeam { get; set; }

        [JsonIgnore]
        public int LeastGoalsConcededAmount { get; set; }

        [JsonIgnore]
        public string LeastGoalsConcededTeam { get; set; }

        [JsonIgnore]
        public int MostGamesWonAmount { get; set; }

        [JsonIgnore]
        public string MostGamesWonTeam { get; set; }

        [JsonIgnore]
        public int MostGamesLostAmount { get; set; }

        [JsonIgnore]
        public string MostGamesLostTeam { get; set; }

        [JsonIgnore]
        public int MostGamesDrawnAmount { get; set; }

        [JsonIgnore]
        public string MostGamesDrawnTeam { get; set; }

        #endregion
    }

    public class StatsCompetitionSeasonTeams
    {
        public string TeamName { get; set; }

        [JsonIgnore]
        public int TeamId { get; set; }

        public int GamesWon { get; set; }

        public int GamesDrawn { get; set; }

        public int GamesLost { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsConceded { get; set; }

        public string GoalDifference => (this.GoalsScored - this.GoalsConceded).ToString("+0;-#");

        [JsonIgnore]
        public int TablePoints => ((this.GamesWon * 3) + (this.GamesDrawn));

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NavigationReferences _navigation { get; set; }
    }
}
