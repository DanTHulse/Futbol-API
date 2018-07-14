﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Futbol.Common.Models.DataModels
{
    public class FootballCompetition
    {
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public string Country { get; set; }

        public int? Tier { get; set; }

        public Uri Reference { get; set; }

        public Uri AllMatches { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<FootballCompetitionSeasons> Seasons { get; set; }
    }

    public class FootballCompetitionSeasons
    {
        public int SeasonId { get; set; }

        public string SeasonPeriod { get; set; }

        public int MatchCount { get; set; }

        public Uri Stats { get; set; }

        public Uri AllMatches { get; set; }
    }
}