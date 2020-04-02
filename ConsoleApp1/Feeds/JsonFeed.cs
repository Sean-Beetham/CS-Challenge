using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeGenerator.Feeds
{

    abstract class JsonFeed
    {
        HttpClient _client;
        protected string _request;

        
        public JsonFeed(string endpoint)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(endpoint);
            _request = "";
        }

        /// <summary>
        /// Execute the query to HttpClient with specified request
        /// </summary>
        /// <returns>A deserialized JSON Object of the returned Data.</returns>
        protected dynamic ExecuteQuery()
        {
            string fetched = Task.FromResult(_client.GetStringAsync(_request).Result).Result;
            return JsonConvert.DeserializeObject<dynamic>(fetched);
        }
    }
}
