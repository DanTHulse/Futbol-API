using System.Collections.Generic;
using Futbol.Common.Models.Football;

namespace Futbol.Importer.Services.Interfaces
{
    public interface IFutbolService : IService
    {
        Team RetrieveTeamByName(string name);

        Season RetrieveSeasonByStartYear(int seasonStartYear);

        Competition RetrieveCompetitionByName(string name);

        void InsertMatches(List<Match> matches);
    }
}
