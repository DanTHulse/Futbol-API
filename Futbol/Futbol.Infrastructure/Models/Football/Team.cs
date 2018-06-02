using System.Collections.Generic;

namespace Futbol.Common.Models.Football
{
    public class Team
    {
        public Team()
        {
            HomeMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
        }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string AlternateTeamName { get; set; }

        public ICollection<Match> HomeMatches { get; set; }

        public ICollection<Match> AwayMatches { get; set; }
    }
}
