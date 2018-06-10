using System;

namespace Futbol.API.DataModels.Stats
{
    public class StatsResults
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string BoxScore { get; set; }

        public DateTime MatchDate { get; set; }

        public string CompetitionSeason { get; set; }

        public Uri MatchData { get; set; }
    }
}
