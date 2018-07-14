using System.Collections.Generic;
using System.Linq;

namespace Futbol.Common.Models.Stats
{
    /// <summary>
    /// Scorigami stats
    /// </summary>
    public class StatsScorigami
    {
        public int TotalGames => Scores.Sum(s => s.Count);

        public int LowestOccurances => Scores.OrderBy(o => o.Count).First().Count;

        public int HighestOccurances => Scores.OrderBy(o => o.Count).Last().Count;

        public IEnumerable<ScorigamiScores> Scores { get; set; }
    }
}
