using System;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class FootballMatch
    {
        [JsonIgnore]
        public int MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public string MatchName { get; set; }

        public string Competition { get; set; }

        public string Result { get; set; }

        public string BoxScore { get; set; }

        public string HalfTimeResult { get; set; }

        public string HalfTimeBoxScore { get; set; }

        public Uri FixtureStats { get; set; }

        public Uri Reference { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FootballMatchData MatchData { get; set; }
    }

    public class FootballMatchData
    {
        [JsonIgnore]
        public int HomeTeamId { get; set; }

        public string HomeTeam { get; set; }

        public Uri HomeTeamReference { get; set; }

        [JsonIgnore]
        public int AwayTeamId { get; set; }

        public string AwayTeam { get; set; }

        public Uri AwayTeamReference { get; set; }

        [JsonIgnore]
        public int CompetitionId { get; set; }

        [JsonIgnore]
        public int SeasonId { get; set; }

        public Uri CompetitionReference { get; set; }

        public Uri SeasonReference { get; set; }

        public int? FullTimeHomeGoals { get; set; }

        public int? FullTimeAwayGoals { get; set; }

        public int? HalfTimeHomeGoals { get; set; }

        public int? HalfTimeAwayGoals { get; set; }

        public int? HomeShots { get; set; }

        public int? HomeShotsOnTarget { get; set; }

        public int? AwayShots { get; set; }

        public int? AwayShotsOnTarget { get; set; }

        public Uri ScoreStats { get; set; }

        public Uri HalfTimeScoreStats { get; set; }
    }
}
