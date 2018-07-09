using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballBetData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFootballBetDataRepository : IRepository
    {
        List<Fixture> ParseFootballBetData(string fileName);
    }
}
