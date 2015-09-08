using Newtonsoft.Json;
using SurfsUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SurfsUp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", "AAAAAAAAAAAAAAAAAAAAABGmhQAAAAAAZfMMsuAvHInzkvJOWP0UUAYydTA%3D6ZnBLNqSyXQ86TC05ykDeRa2WIErykCJm2m2g3dRFHL2pM8I1h"));
            Uri uri = new Uri("https://api.twitter.com/1.1/search/tweets.json?q=%23surfsUp");
            HttpResponseMessage response = await client.GetAsync(uri);
            String responseString = await response.Content.ReadAsStringAsync();

            TwitterObject twitterObject = JsonConvert.DeserializeObject<TwitterObject>(responseString);

            CoordinatesModel model = new CoordinatesModel();
            foreach (var status in twitterObject.statuses)
            {
                if (status.geo != null && status.geo.coordinates != null && status.geo.coordinates.Any())
                    model.Coordinates.Add(status.geo.coordinates);
            }

            string nextUrl = twitterObject.search_metadata.next_results;

            for (int i = 0; i < 20;i++) 
            {
                HttpClient client1 = new HttpClient();
                client1.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", "AAAAAAAAAAAAAAAAAAAAABGmhQAAAAAAZfMMsuAvHInzkvJOWP0UUAYydTA%3D6ZnBLNqSyXQ86TC05ykDeRa2WIErykCJm2m2g3dRFHL2pM8I1h"));
                Uri uri1 = new Uri("https://api.twitter.com/1.1/search/tweets.json" + nextUrl);
                HttpResponseMessage response1 = await client.GetAsync(uri1);
                String responseString1 = await response1.Content.ReadAsStringAsync();

                TwitterObject twitterObject1 = JsonConvert.DeserializeObject<TwitterObject>(responseString1);

                foreach (var status in twitterObject1.statuses)
                {
                    if (status.geo != null && status.geo.coordinates != null && status.geo.coordinates.Any())
                        model.Coordinates.Add(status.geo.coordinates);
                }

                nextUrl = twitterObject1.search_metadata.next_results;
            }



            return View(model);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
