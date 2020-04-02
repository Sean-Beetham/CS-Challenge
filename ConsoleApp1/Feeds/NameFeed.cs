#define TESTDATA

using Newtonsoft.Json;
using System;


namespace JokeGenerator.Feeds
{
    class NameFeed: JsonFeed
    {

        public NameFeed() : base("http://uinames.com/api/") { }

        /// <summary>
        /// Retrieve a random name from the website uinames.com
        /// </summary>
        /// <returns>A tuple of strings with a name and a surname</returns>
        public Tuple<string, string> GetName()
        {
            _request = "";
#if !TESTDATA
            dynamic results = ExecuteQuery();
#else
            dynamic results = GetTestData();
#endif
            return new Tuple<string, string>(results.name.Value, results.surname.Value);
        }

#if TESTDATA

        /// <summary>
        /// Test data function that provides a random json return
        /// </summary>
        /// <returns>A randomly generated JSON object to mirror the website</returns>
        private dynamic GetTestData()
        {
            Random x = new Random();
            string[] data = { "{\"name\":\"Sean\",\"surname\":\"Beetham\",\"gender\":\"m\",\"region\":\"Canada\"}",
                        "{\"name\":\"Steph\",\"surname\":\"O'Neill\",\"gender\":\"f\",\"region\":\"Canada\"}",
                        "{\"name\":\"Meagan\",\"surname\":\"Geary\",\"gender\":\"f\",\"region\":\"Canada\"}",
                        "{\"name\":\"Andrew\",\"surname\":\"Fox\",\"gender\":\"m\",\"region\":\"Canada\"}"};
            return JsonConvert.DeserializeObject<dynamic>(data[x.Next() % 4]);
        }
#endif
    }
}
