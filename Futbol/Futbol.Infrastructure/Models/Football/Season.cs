using System.Collections.Generic;

namespace Futbol.Common.Models.Football
{
    public class Season
    {
        public Season()
        {
            Matches = new HashSet<Match>();
        }

        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
