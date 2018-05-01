using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.API.DataModels
{
    public class FootballMatch
    {
        public int MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public string MatchName { get; set; }

        public string Competition { get; set; }

        public string Result { get; set; }

        public string BoxScore { get; set; }

        public string HalfTimeResult { get; set; }

        public string HalfTimeBoxScore { get; set; }

        public FootballMatchData MatchData { get; set; }
    }

    public class FootballMatchData
    {
        public int HomeTeamId { get; set; }

        public string HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeam { get; set; }

        public int CompetitionId { get; set; }

        public int SeasonId { get; set; }

        public int FullTimeHomeGoals { get; set; }

        public int FullTimeAwayGoals { get; set; }

        public int? HalfTimeHomeGoals { get; set; }

        public int? HalfTimeAwayGoals { get; set; }
    }
}
