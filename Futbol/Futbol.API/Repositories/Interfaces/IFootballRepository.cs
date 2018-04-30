using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futbol.API.DataModels;
using Futbol.Common.Models.Football;

namespace Futbol.API.Repositories.Interfaces
{
    public interface IFootballRepository : IRepository
    {
        Task<IEnumerable<Match>> GetMatches(FootballFilter filter);

        Task<IEnumerable<Match>> GetAllMatches();
    }
}
