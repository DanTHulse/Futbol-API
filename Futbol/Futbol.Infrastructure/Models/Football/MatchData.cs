using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Common.Models.Football
{
    public class MatchData
    {
        public int MatchId { get; set; }

        public int FTHomeGoals { get; set; }

        public int FTAwayGoals { get; set; }

        public string FTResult { get; set; }

        public int HTHomeGoals { get; set; }

        public int HTAwayGoals { get; set; }

        public string HTResult { get; set; }

        public int HomeShots { get; set; }

        public int AwayShots { get; set; }

        public int HomeShotsOnTarget { get; set; }

        public int AwayShotsOnTarget { get; set; }
    }
}
