using System;

namespace Futbol.Common.Models.Stats
{
    public class StatsRecord
    {
        public int GamesPlayed => (this.GamesWon + this.GamesLost + this.GamesDrawn);

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int GamesDrawn { get; set; }

        public decimal WinRatio => Math.Truncate(((decimal)this.GamesWon / (decimal)this.GamesPlayed) * 1000m) / 1000m;

        public decimal LossRatio => Math.Truncate(((decimal)this.GamesLost / (decimal)this.GamesPlayed) * 1000m) / 1000m;

        public decimal DrawRatio => Math.Truncate(((decimal)1 - this.WinRatio - this.LossRatio) * 1000m) / 1000m;

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public string GoalDifference => (this.GoalsFor - this.GoalsAgainst).ToString("+0;-#");
    }
}
