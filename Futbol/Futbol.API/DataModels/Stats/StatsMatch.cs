﻿using System;
using Newtonsoft.Json;

namespace Futbol.API.DataModels.Stats
{
    /// <summary>
    /// Stats for matches
    /// </summary>
    public class StatsMatch
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string BoxScore { get; set; }

        public DateTime MatchDate { get; set; }

        public string CompetitionSeason { get; set; }

        [JsonIgnore]
        public int MatchId { get; set; }

        public Uri MatchData { get; set; }
    }
}
