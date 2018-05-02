using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballData;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;
using RestSharp;

namespace Futbol.Importer.Repositories
{
    public class FootballDataRepository : IFootballDataRepository
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly Configuration config;

        /// <summary>
        /// The rest sharp service
        /// </summary>
        private readonly IRestSharpService restSharpService;

        /// <summary>
        /// The football data API URL
        /// </summary>
        private readonly string footballDataApiUrl;

        /// <summary>
        /// The football data API key
        /// </summary>
        private readonly string footballDataApiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballDataRepository"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="restSharpService">The rest sharp service.</param>
        public FootballDataRepository(Configuration config, IRestSharpService restSharpService)
        {
            this.config = config;
            this.restSharpService = restSharpService;

            this.footballDataApiUrl = this.config.AppSettings.FootballDataApiUrl;
            this.footballDataApiKey = this.config.AppSettings.FootballDataApiKey;
        }

        /// <summary>
        /// Retrives the competitions by season.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        /// <returns></returns>
        public List<Competitions> RetriveCompetitionsBySeason(int seasonStartYear)
        {
            this.restSharpService.Url = $"{this.footballDataApiUrl}/competitions/";
            this.restSharpService.ClearParameters();
            this.restSharpService.AddParameter("season", seasonStartYear);
            this.restSharpService.AddParameter("X-Auth-Token", this.footballDataApiKey, ParameterType.HttpHeader);

            var result = this.restSharpService.Execute<List<Competitions>>();

            return result.Data;
        }

        /// <summary>
        /// Retrieves the fixtures for competition.
        /// </summary>
        /// <param name="competitionId">The competition identifier.</param>
        /// <returns></returns>
        public FixturesHeader RetrieveFixturesForCompetition(int competitionId)
        {
            this.restSharpService.Url = $"{this.footballDataApiUrl}/competitions/{competitionId}/fixtures/";
            this.restSharpService.ClearParameters();
            this.restSharpService.AddParameter("X-Auth-Token", this.footballDataApiKey, ParameterType.HttpHeader);

            var result = this.restSharpService.Execute<FixturesHeader>();

            return result.Data;
        }
    }
}
