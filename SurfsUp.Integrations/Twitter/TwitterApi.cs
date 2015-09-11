using Newtonsoft.Json;
using SurfsUp.Types.Integrations.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUp.Integrations.Twitter
{
    public class TwitterApi
    {
        private static string token = "AAAAAAAAAAAAAAAAAAAAABGmhQAAAAAAZfMMsuAvHInzkvJOWP0UUAYydTA%3D6ZnBLNqSyXQ86TC05ykDeRa2WIErykCJm2m2g3dRFHL2pM8I1h";

        public static async Task<StatusSearchResponse> SearchStatus(string query)
        {
            StatusSearchResponse searchResponse = new StatusSearchResponse();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
                Uri uri = new Uri(string.Format("https://api.twitter.com/1.1/search/tweets.json{0}", query));
                HttpResponseMessage response = await client.GetAsync(uri);
                String responseString = await response.Content.ReadAsStringAsync();

                searchResponse = JsonConvert.DeserializeObject<StatusSearchResponse>(responseString);
            }

            return searchResponse;
        }

    }
}
