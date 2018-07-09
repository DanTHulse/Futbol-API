using System.Collections.Generic;
using Futbol.Importer.DataModels.FootballData;
using Futbol.Importer.Repositories.Interfaces;
using Futbol.Importer.Services.Interfaces;
using RestSharp;

namespace Futbol.Importer.Repositories
{
    public class FootballDataRepository : IFootballDataRepository
    {
        private readonly Configuration config;

        private readonly IRestSharpService restSharpService;

        private readonly string footballDataApiUrl;

        private readonly string footballDataApiKey;

        public FootballDataRepository(Configuration config, IRestSharpService restSharpService)
        {
            this.config = config;
            this.restSharpService = restSharpService;

            this.footballDataApiUrl = this.config.AppSettings.FootballDataApiUrl;
            this.footballDataApiKey = this.config.AppSettings.FootballDataApiKey;
        }

        public List<Competitions> RetriveCompetitionsBySeason(int seasonStartYear)
        {
            this.restSharpService.Url = $"{this.footballDataApiUrl}/competitions/";
            this.restSharpService.ClearParameters();
            this.restSharpService.AddParameter("season", seasonStartYear);
            this.restSharpService.AddParameter("X-Auth-Token", this.footballDataApiKey, ParameterType.HttpHeader);

            var result = this.restSharpService.Execute<List<Competitions>>();

            return result.Data;
        }

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
