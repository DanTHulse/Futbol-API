using System.Collections.Generic;
using System.Net.Http;
using RestSharp;

namespace Futbol.Importer.Services.Interfaces
{
    public interface IRestSharpService : IService
    {
        string Url { get; set; }

        object Body { get; set; }

        DataFormat DataFormat { get; set; }

        HttpMethod RequestMethod { get; set; }

        IRestResponse<T> Execute<T>(bool authenticatedRequest = false, string userName = null, string password = null) where T : new();

        IRestResponse Execute(bool authenticatedRequest = false, string userName = null, string password = null);

        void ClearParameters();

        void AddParameter(string parameterName, object parameterValue, ParameterType parameterType = ParameterType.QueryString);

        void AddParameterRange(IDictionary<string, object> parameterRange, ParameterType parameterType = ParameterType.QueryString);
    }
}
