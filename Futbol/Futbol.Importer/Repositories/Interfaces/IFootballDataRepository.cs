using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballData;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFootballDataRepository : IRepository
    {
        List<Competitions> RetriveCompetitionsBySeason(int seasonStartYear);

        FixturesHeader RetrieveFixturesForCompetition(int competitionId);
    }
}
