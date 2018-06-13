using CsvHelper.Configuration;
using Futbol.Importer.DataModels.EngSoccorData;
using Futbol.Importer.DataModels.FootballBetData;

namespace Futbol.Importer.Helpers
{
    public sealed class FootballBetMap : ClassMap<Fixture>
    {
        public FootballBetMap()
        {
            Map(m => m.Division).Name("Div", "League");
            Map(m => m.Season).Name("Season");
            Map(m => m.MatchDate).Name("Date");
            Map(m => m.HomeTeam).Name("HomeTeam", "Home", "HT");
            Map(m => m.AwayTeam).Name("AwayTeam", "Away", "AT");
            Map(m => m.FullTimeHomeGoals).Name("FTHG", "HG");
            Map(m => m.FullTimeAwayGoals).Name("FTAG", "AG");
            Map(m => m.FullTimeResult).Name("FTR", "Res");
            Map(m => m.HalfTimeHomeGoals).Name("HTHG");
            Map(m => m.HalfTimeAwayGoals).Name("HTAG");
            Map(m => m.HalfTimeResult).Name("HTR");
            Map(m => m.HomeShots).Name("HS");
            Map(m => m.AwayShots).Name("AS");
            Map(m => m.HomeShotsOnTarget).Name("HST");
            Map(m => m.AwayShotsOnTarget).Name("AST");
        }
    }

    public sealed class EngSoccorDataMap : ClassMap<Fixtures>
    {
        public EngSoccorDataMap()
        {
            Map(m => m.Division).Name("division");
            Map(m => m.Season).Name("Season");
            Map(m => m.DateString).Name("Date");
            Map(m => m.HomeTeam).Name("home");
            Map(m => m.AwayTeam).Name("visitor", "away");
            Map(m => m.FTHomeGoals).Name("hgoal");
            Map(m => m.FTAwayGoals).Name("vgoal", "agoal");
            Map(m => m.FTScore).Name("FT");
            Map(m => m.Tier).Name("tier");
        }
    }
}
