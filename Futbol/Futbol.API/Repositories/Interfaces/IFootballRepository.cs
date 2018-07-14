﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Futbol.Common.Models.DataModels;
using Futbol.Common.Models.Football;

namespace Futbol.API.Repositories.Interfaces
{
    public interface IFootballRepository : IRepository
    {
        Task<IEnumerable<Match>> GetMatches(FootballFilter filter, int page, int pageSize);

        Task<Match> GetMatchById(int Id);

        Task<IEnumerable<FootballCompetitionSeasons>> GetCompetitionSeasons(int competitionId);

        Task<T> GetById<T>(int Id) where T : class;

        Task<IEnumerable<T>> Get<T>() where T : class;
    }
}
