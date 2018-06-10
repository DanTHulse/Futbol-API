namespace Futbol.Common.Models.Football
{
    public class MatchData
    {
        public int MatchDataId { get; set; }

        public int MatchId { get; set; }

        public int? FTHomeGoals { get; set; }

        public int? FTAwayGoals { get; set; }

        public string FTResult { get; set; }

        public int? HTHomeGoals { get; set; }

        public int? HTAwayGoals { get; set; }

        public string HTResult { get; set; }

        public int? HomeShots { get; set; }

        public int? AwayShots { get; set; }

        public int? HomeShotsOnTarget { get; set; }

        public int? AwayShotsOnTarget { get; set; }

        public int? FTGoals_1 => this.FTHomeGoals >= this.FTAwayGoals ? this.FTHomeGoals : this.FTAwayGoals;

        public int? FTGoals_2 => this.FTHomeGoals >= this.FTAwayGoals ? this.FTAwayGoals : this.FTHomeGoals;

        public int? HTGoals_1 => this.HTHomeGoals >= this.HTAwayGoals ? this.HTHomeGoals : this.HTAwayGoals;

        public int? HTGoals_2 => this.HTHomeGoals >= this.HTAwayGoals ? this.HTAwayGoals : this.HTHomeGoals;

        public Match Match { get; set; }
    }
}
