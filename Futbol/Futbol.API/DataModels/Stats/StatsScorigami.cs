using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Models.Stats;

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
        public IEnumerable<ScorigamiScores> Scores { get; set; }
    }
}
