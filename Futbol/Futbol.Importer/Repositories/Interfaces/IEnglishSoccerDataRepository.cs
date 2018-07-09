using System.Collections.Generic;
using Futbol.Importer.DataModels.EngSoccorData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IEnglishSoccerDataRepository : IRepository
    {
        List<Fixtures> ParseFootballBetData(string fileName);
    }
}
