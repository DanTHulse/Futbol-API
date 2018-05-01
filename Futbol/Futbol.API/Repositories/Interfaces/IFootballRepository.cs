﻿using System;
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
        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A list of all matches based on the filter</returns>
        Task<IEnumerable<Match>> GetMatches(FootballFilter filter);

        /// <summary>
        /// Gets all matches.
        /// </summary>
        /// <returns>A list of all matches</returns>
        Task<IEnumerable<Match>> GetAllMatches();

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Record by Id</returns>
        Task<T> GetById<T>(int Id) where T : class;

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns>List of all records</returns>
        Task<IEnumerable<T>> Get<T>() where T : class;
    }
}
