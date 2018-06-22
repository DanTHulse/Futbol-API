using System;

namespace Futbol.Importer.DataModels.EngSoccorData
{
    public class Fixtures
    {
        public string DateString { get; set; }

        public DateTime? Date => this.DateString == "N/A" || this.DateString == "NA" || this.DateString == "" ? new DateTime(this.Season, 1, 1) : DateTime.Parse(this.DateString);

        public int Season { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string FTScore { get; set; }

        public int? FTHomeGoals { get; set; }

        public int? FTAwayGoals { get; set; }

        public string Division { get; set; }

        public string Tier { get; set; }

        public string Result { get; set; }
    }
}
