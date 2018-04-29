using System.Collections.Generic;

namespace Futbol.Common.Models.Football
{
    public class Competition
    {
        public Competition()
        {
            Matches = new HashSet<Match>();
        }

        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
