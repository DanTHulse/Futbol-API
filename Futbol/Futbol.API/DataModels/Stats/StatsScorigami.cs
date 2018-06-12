using System;
using System.Collections.Generic;
using System.Linq;

namespace Futbol.API.DataModels.Stats
{
    /// <summary>
    /// Scorigami stats
    /// </summary>
    public class StatsScorigami
    {
        /// <summary>
        /// Gets the total games.
        /// </summary>
        public int TotalGames => Scores.Sum(s => s.Count);

        /// <summary>
        /// Gets the lowest score.
        /// </summary>
        public int LowestOccurances => Scores.OrderBy(o => o.Count).First().Count;

        /// <summary>
        /// Gets the highest occurances.
        /// </summary>
        public int HighestOccurances => Scores.OrderBy(o => o.Count).Last().Count;

        /// <summary>
        /// Gets or sets the scores.
        /// </summary>
        public IEnumerable<StatsScorigamiScores> Scores { get; set; }
    }

    /// <summary>
    /// Score stats for scorigami
    /// </summary>
    public class StatsScorigamiScores
    {
        /// <summary>
        /// Gets or sets the box score.
        /// </summary>
        public string BoxScore { get; set; }

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
