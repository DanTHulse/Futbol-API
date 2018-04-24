using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Common.Models.Football
{
    public class Match
    {
        public int MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public int SeasonId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }
    }
}
