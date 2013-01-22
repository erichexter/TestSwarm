using System.Collections.Generic;
using RestSharp;
using nTestSwarm.Interface;

namespace nTestSwarm.Application.Services
{
    public interface IJobDescriptionClient
    {
        IJobDescriptor GetFrom(string url, string[] correlation);
    }

    public class JobDescriptionClient : IJobDescriptionClient
    {
        public IJobDescriptor GetFrom(string url, string[] correlation)
        {
            var restClient = new RestClient(url);

            var request = new RestRequest(Method.GET);

            if (correlation == null)
            {
                correlation = new[] {string.Empty};
            }

            foreach (var item in correlation)
            {
                request.AddParameter("correlation", item, ParameterType.GetOrPost);
            }

            request.RequestFormat = DataFormat.Json;

            var restResponse = restClient.Execute<JobDescriptor>(request);

            if (restResponse.ErrorException != null)
                throw restResponse.ErrorException;

            return restResponse.Data;
        }

        class JobDescriptor : IJobDescriptor
        {
            public List<RunDescriptor> Runs { get; set; }

            public string SuiteId { get; set; }

            public string Name { get; private set; }
            IEnumerable<IRunDescriptor> IJobDescriptor.Runs { get { return Runs; } }
        }

        class RunDescriptor : IRunDescriptor
        {
            public string Url { get; private set; }
            public string Name { get; private set; }
        }
    }
}