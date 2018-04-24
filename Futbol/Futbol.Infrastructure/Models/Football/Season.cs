using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Common.Models.Football
{
    public class Season
    {
        public int SeasonId { get; set; }

        public int CompetitionId { get; set; }

        public string SeasonPeriod { get; set; }
    }
}
