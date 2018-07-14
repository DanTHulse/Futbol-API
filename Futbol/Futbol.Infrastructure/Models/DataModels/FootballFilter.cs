namespace Futbol.Common.Models.DataModels
{
    public class FootballFilter
    {
        public int? CompetitionId { get; set; }

        public int? SeasonId { get; set; }

        public int? TeamId { get; set; }

        public int? TeamId_2 { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public int? BoxScoreFirst { get; set; }

        public int? BoxScoreSecond { get; set; }

        public int? HalftimeBoxScoreFirst { get; set; }

        public int? HalftimeBoxScoreSecond { get; set; }

        public string FullTimeResult { get; set; }

        public string HalfTimeResult { get; set; }

        public int? MatchId { get; set; }
    }
}
