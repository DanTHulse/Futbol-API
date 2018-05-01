using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.API.DataModels
{
    public class FootballCompetition
    {
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }
    }
}
