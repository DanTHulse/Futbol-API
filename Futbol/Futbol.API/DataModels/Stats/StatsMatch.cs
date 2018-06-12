using System;
using Newtonsoft.Json;

namespace Futbol.API.DataModels.Stats
{
    /// <summary>
    /// Stats for matches
    /// </summary>
    public class StatsMatch
    {
        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        public string HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the away team.
        /// </summary>
        public string AwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the box score.
        /// </summary>
        public string BoxScore { get; set; }

        /// <summary>
        /// Gets or sets the match date.
        /// </summary>
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// Gets or sets the competition season.
        /// </summary>
        public string CompetitionSeason { get; set; }

        /// <summary>
        /// Gets or sets the match identifier.
        /// </summary>
        [JsonIgnore]
        public int MatchId { get; set; }

        /// <summary>
        /// Gets or sets the match data.
        /// </summary>
        public Uri MatchData { get; set; }
    }
}
