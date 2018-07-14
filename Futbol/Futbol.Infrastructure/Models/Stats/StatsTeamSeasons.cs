using System;
using System.Collections.Generic;

namespace Futbol.Common.Models.Stats
{
    public class StatsTeamSeasons
    {
        public string TeamName { get; set; }

        public IEnumerable<StatsTeamSeason> Seasons { get; set; }
    }

    public class StatsTeamSeason
    {
        public StatsRecord SeasonRecord { get; set; }

        public Uri TeamSeasonStats { get; set; }

        public Uri AllMatches { get; set; }

        public IEnumerable<StatsTeamCompetitions> Competitions { get; set; }
    }

    public class StatsTeamCompetitions
    {
        public StatsRecord CompetitionRecord { get; set; }

        public Uri TeamCompetitionRecord { get; set; }

        public Uri AllMatches { get; set; }
    }
}
