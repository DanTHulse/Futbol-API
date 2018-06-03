using CsvHelper.Configuration;
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
}
