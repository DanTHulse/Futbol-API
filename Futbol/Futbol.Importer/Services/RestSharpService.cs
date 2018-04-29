using System;
using System.Collections.Generic;
using System.Net.Http;
using Futbol.Importer.Services.Interfaces;
using RestSharp;
using RestSharp.Authenticators;

namespace Futbol.Importer.Services
{
    public class RestSharpService : IRestSharpService
    {
        private List<Parameter> requestParameters;

        public string Url { get; set; }

        public object Body { get; set; }

        public DataFormat DataFormat { get; set; } = DataFormat.Json;

        public HttpMethod RequestMethod { get; set; } = HttpMethod.Get;

        public RestSharpService()
        {
            this.requestParameters = new List<Parameter>();
        }

        public IRestResponse<T> Execute<T>(bool authenticatedRequest = false, string userName = null, string password = null) where T : new()
        {
            var client = this.GetClient(this.Url);

            if (authenticatedRequest)
            {
                client.Authenticator = new HttpBasicAuthenticator(userName, password);
            }

            var request = this.BuildRequest();

            return client.Execute<T>(request);
        }

        public IRestResponse Execute(bool authenticatedRequest = false, string userName = null, string password = null)
        {
            var client = this.GetClient(this.Url);

            if (authenticatedRequest)
            {
                client.Authenticator = new HttpBasicAuthenticator(userName, password);
            }

            var request = this.BuildRequest();

            return client.Execute(request);
        }

        public void ClearParameters()
        {
            this.requestParameters = new List<Parameter>();
        }

        public void AddParameter(string parameterName, object parameterValue, ParameterType parameterType = ParameterType.QueryString)
        {
            this.requestParameters.Add(new Parameter { Name = parameterName, Value = parameterValue, Type = parameterType });
        }

        public void AddParameterRange(IDictionary<string, object> parameterRange, ParameterType parameterType = ParameterType.QueryString)
        {
            foreach (var parameter in parameterRange)
            {
                this.requestParameters.Add(new Parameter { Name = parameter.Key, Value = parameter.Value, Type = parameterType });
            }
        }

        private IRestClient GetClient(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url), "Request Url cannot be null");
            }

            return new RestClient(url);
        }

        private IRestRequest BuildRequest()
        {
            if (this.RequestMethod == null)
            {
                throw new ArgumentNullException(nameof(RequestMethod), "Request method cannot be null");
            }

            var request = new RestRequest((Method)Enum.Parse(typeof(Method), this.RequestMethod.Method))
            {
                RequestFormat = this.DataFormat
            };

            if (this.Body != null)
            {
                request.AddBody(this.Body);
            }

            foreach (var parameter in this.requestParameters)
            {
                request.AddParameter(parameter);
            }

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            return request;
        }
    }
}
