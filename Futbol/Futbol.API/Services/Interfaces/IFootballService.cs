using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futbol.Common.Models.Football;

namespace Futbol.API.Services.Interfaces
{
    public interface IFootballService : IService
    {
        Task<IEnumerable<Match>> GetMatches(string competitionIds, string seasonIds, string teamIds,
                                            int? homeTeamId, int? awayTeamId, string boxScore, string halftimeBoxScore);

        Task<IEnumerable<Match>> GetAllMatches();
    }
}
