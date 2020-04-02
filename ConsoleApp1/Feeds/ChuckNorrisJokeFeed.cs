using Newtonsoft.Json;

namespace JokeGenerator.Feeds
{
    class ChuckNorrisJokeFeed : JsonFeed
    {
        //"Chuck Norris".Length is a constant value that won't change.
        const int name_length = 12; 

        public ChuckNorrisJokeFeed() : base("https://api.chucknorris.io") { }


        /// <summary>
        /// This function retreives a random joke from the site, api.chucknorris.io,
        /// from a specified category (optional), and replaces the name with a provided
        /// name (optional)
        /// </summary>
        /// <param name="firstname">First name of the person to replace with (Optional)</param>
        /// <param name="lastname">Last name of the person to replace with (Optional)</param>
        /// <param name="category">Category to choose from (Optional)</param>
        /// <returns>A string with the randomly selected joke with the name replaced if requested</returns>
        public string GetRandomJoke(string firstname, string lastname, string category)
        {
            _request = "jokes/random";
            //If a category was specified add it to the request
            if(!string.IsNullOrEmpty(category))
            {
                _request += "?category=" + category;
            }
            dynamic fetched = ExecuteQuery();
            string joke = fetched.value;

            //If a name was requested replace Chuck Norris with provided Name.
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                int nameIndex = joke.IndexOf("Chuck Norris");
                joke = joke.Substring(0, nameIndex) + firstname + " " + lastname +
                    joke.Substring(nameIndex + name_length);
            }
            return joke;
        }

        /// <summary>
        /// Retrieve a list of categories available from the website
        /// </summary>
        /// <returns>An array of strings containing all available categories</returns>
        public string[] GetCategories()
        {
            _request = "jokes/categories";
            dynamic retvalue = ExecuteQuery();
            string[] categories = retvalue.ToObject<string[]>();
            return categories;
        }
    }
}
