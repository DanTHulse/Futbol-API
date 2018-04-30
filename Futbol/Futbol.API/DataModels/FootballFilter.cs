using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.API.DataModels
{
    public class FootballFilter
    {
        public int[] CompetitionIds { get; set; }

        public int[] SeasonIds { get; set; }

        public int[] TeamIds { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public int? BoxScoreFirst { get; set; }

        public int? BoxScoreSecond { get; set; }

        public int? HalftimeBoxScoreFirst { get; set; }

        public int? HalftimeBoxScoreSecond { get; set; }
    }
}
