using System;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class NavigationReferences
    {
        [JsonProperty("_match", NullValueHandling = NullValueHandling.Ignore)]
        public Uri MatchDetails { get; set; }

        [JsonProperty("_homeTeam", NullValueHandling = NullValueHandling.Ignore)]
        public Uri HomeTeamDetails { get; set; }

        [JsonProperty("_awayTeam", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AwayTeamDetails { get; set; }

        [JsonProperty("_team", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TeamDetails { get; set; }

        [JsonProperty("_competition", NullValueHandling = NullValueHandling.Ignore)]
        public Uri CompetitionDetails { get; set; }

        [JsonProperty("_season", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SeasonDetails { get; set; }

        [JsonProperty("_fixture", NullValueHandling = NullValueHandling.Ignore)]
        public Uri FixtureStats { get; set; }

        [JsonProperty("_reverseFixture", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ReverseFixtureStats { get; set; }

        [JsonProperty("_teamStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TeamStats { get; set; }

        [JsonProperty("_teamSeasonStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TeamSeasonStats { get; set; }

        [JsonProperty("_teamCompetitionStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri TeamCompetitionStats { get; set; }

        [JsonProperty("_seasonStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SeasonStats { get; set; }

        [JsonProperty("_competitionStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri CompetitionStats { get; set; }

        [JsonProperty("_table", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SeasonTable { get; set; }

        [JsonProperty("_scoreStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ScoreStats { get; set; }

        [JsonProperty("_halfTimeScoreStats", NullValueHandling = NullValueHandling.Ignore)]
        public Uri HalfTimeScoreStats { get; set; }

        [JsonProperty("_allMatches", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AllMatches { get; set; }
    }
}
