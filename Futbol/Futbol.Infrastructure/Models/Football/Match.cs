using System;

namespace Futbol.Common.Models.Football
{
    public class Match
    {
        public int MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public int CompetitionId { get; set; }

        public int SeasonId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public MatchData MatchData { get; set; }

        public Competition Competition { get; set; }

        public Season Season { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
    }
}
