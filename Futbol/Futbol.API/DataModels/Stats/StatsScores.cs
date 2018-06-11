using System;
using Newtonsoft.Json;

namespace Futbol.API.DataModels.Stats
{
    /// <summary>
    /// The stats for a chosen scoreline
    /// </summary>
    public class StatsScores
    {
        /// <summary>
        /// Gets or sets the box score.
        /// </summary>
        public string BoxScore { get; set; }

        /// <summary>
        /// Gets or sets the first match the scoreline occured.
        /// </summary>
        public StatsMatch FirstMatch { get; set; }

        /// <summary>
        /// Gets or sets the last match the scoreline occured.
        /// </summary>
        public StatsMatch LastMatch { get; set; }

        /// <summary>
        /// Gets or sets the count of the number of occurences of the scoreline.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the first goal value.
        /// </summary>
        [JsonIgnore]
        public int Goals_1 { get; set; }

        /// <summary>
        /// Gets or sets the second goal value.
        /// </summary>
        [JsonIgnore]
        public int Goals_2 { get; set; }

        /// <summary>
        /// Gets or sets the URL for all matches with this scoreline.
        /// </summary>
        public Uri AllMatches { get; set; }
    }
}
