using System;
using Newtonsoft.Json;

namespace Futbol.Common.Models.Stats
{
    public class ScorigamiScores
    {
        /// <summary>
        /// Gets or sets the box score.
        /// </summary>
        public string BoxScore => $"{this.Score_1}-{this.Score_2}";

        /// <summary>
        /// Gets or sets the score 1.
        /// </summary>
        [JsonIgnore]
        public int? Score_1 { get; set; }

        /// <summary>
        /// Gets or sets the score 2.
        /// </summary>
        [JsonIgnore]
        public int? Score_2 { get; set; }

        /// <summary>
        /// Gets or sets the count of the number of occurences of the scoreline.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the URL for stats with this scoreline.
        /// </summary>
        public Uri ScoreStats { get; set; }
    }
}
