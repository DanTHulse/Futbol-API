using System;

namespace Futbol.Common.Models.DataModels
{
    public class FootballTeam
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public Uri Stats { get; set; }

        public Uri AllMatches { get; set; }
    }
}
