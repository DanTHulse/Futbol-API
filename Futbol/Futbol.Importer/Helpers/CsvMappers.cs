using CsvHelper.Configuration;
using Futbol.Importer.DataModels.FootballBetData;

namespace Futbol.Importer.Helpers
{
    public sealed class FootballBetMap : ClassMap<Fixture>
    {
        public FootballBetMap()
        {
            Map(m => m.Division).Name("Div");
            Map(m => m.MatchDate).Name("Date");
            Map(m => m.HomeTeam).Name("HomeTeam");
            Map(m => m.AwayTeam).Name("AwayTeam");
            Map(m => m.FullTimeHomeGoals).Name("FTHG");
            Map(m => m.FullTimeAwayGoals).Name("FTAG");
            Map(m => m.FullTimeResult).Name("FTR");
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
