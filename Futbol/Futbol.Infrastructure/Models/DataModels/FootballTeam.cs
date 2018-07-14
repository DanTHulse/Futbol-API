using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class FootballTeam
    {
        [JsonIgnore]
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        [JsonProperty("TeamDetails")]
        public Uri Reference { get; set; }

        public Uri Stats { get; set; }

        public Uri AllMatches { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<FootballTeamSeasons> CompetitionSeasons { get; set; }
    }

    public class FootballTeamSeasons
    {
        public string SeasonPeriod { get; set; }

        public int MatchCount { get; set; }

        public Uri Stats { get; set; }

        public Uri AllMatches { get; set; }

        public IEnumerable<FootballTeamSeasonCompetitions> Competitions { get; set; }
    }

    public class FootballTeamSeasonCompetitions
    {
        public string CompetitionName { get; set; }

        public int MatchCount { get; set; }

        public Uri Stats { get; set; }

        public Uri AllMatches { get; set; }
    }

    public class FootballCompetitionSeasonsTeam_Data
    {
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public int MatchCount { get; set; }
    }
}
