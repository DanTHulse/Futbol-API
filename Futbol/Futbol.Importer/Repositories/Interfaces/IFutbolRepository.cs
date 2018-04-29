using System.Collections.Generic;
using Futbol.Common.Models.Football;

namespace Futbol.Importer.Repositories.Interfaces
{
    public interface IFutbolRepository : IRepository
    {
        Team RetrieveTeamByName(string name);

        Season RetrieveSeasonByStartYear(int seasonStartYear);

        Competition RetrieveCompetitionByName(string name);

        void Add<T>(T record) where T : class;

        void InsertMatches(List<Match> records);
    }
}
