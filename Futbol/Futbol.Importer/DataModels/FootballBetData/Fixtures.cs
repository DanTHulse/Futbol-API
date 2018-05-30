using System;

namespace Futbol.Importer.DataModels.FootballBetData
{
    public class Fixture
    {
        public string Division { get; set; }

        public DateTime MatchDate { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int FullTimeHomeGoals { get; set; }

        public int FullTimeAwayGoals { get; set; }

        public string FullTimeResult { get; set; }

        public int? HalfTimeHomeGoals { get; set; }

        public int? HalfTimeAwayGoals { get; set; }

        public string HalfTimeResult { get; set; }

        public int? HomeShots { get; set; }

        public int? AwayShots { get; set; }

        public int? HomeShotsOnTarget { get; set; }

        public int? AwayShotsOnTarget { get; set; }
    }
}
