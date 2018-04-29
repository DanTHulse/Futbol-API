namespace Futbol.Common.Models.Football
{
    public class MatchData
    {
        public int MatchDataId { get; set; }

        public int MatchId { get; set; }

        public int FTHomeGoals { get; set; }

        public int FTAwayGoals { get; set; }

        public string FTResult { get; set; }

        public int? HTHomeGoals { get; set; }

        public int? HTAwayGoals { get; set; }

        public string HTResult { get; set; }

        public int? HomeShots { get; set; }

        public int? AwayShots { get; set; }

        public int? HomeShotsOnTarget { get; set; }

        public int? AwayShotsOnTarget { get; set; }

        public Match Match { get; set; }
    }
}
