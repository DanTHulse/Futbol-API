using System;
using System.Collections.Generic;

namespace Futbol.API.DataModels
{
    public class FootballCompetition
    {
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }

        public Uri AllMatches { get; set; }
    }

    public class FootballCompetitionSeason
    {
        public FootballCompetition Competition { get; set; }

        public IEnumerable<FootballSeason> Seasons { get; set; }
    }
}
