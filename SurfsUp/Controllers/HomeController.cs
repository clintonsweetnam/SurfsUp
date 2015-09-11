using Newtonsoft.Json;
using SurfsUp.Integrations.Twitter;
using SurfsUp.Interfaces.Logging;
using SurfsUp.Logging;
using SurfsUp.Models;
using SurfsUp.Types;
using SurfsUp.Types.Integrations.Twitter;
using SurfsUpRepositorys.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ILogger logger;
        private readonly StatusRepository statusRepository;

        public HomeController(ILogger logger)
        {
            statusRepository = new StatusRepository();
            this.logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            string nextUrl = "";

            logger.LogInfo(Enums.LogType.Logging, "Entering HomeController.Index");
            Stopwatch timer = Stopwatch.StartNew();

            CoordinatesModel model = new CoordinatesModel();

            IList<Status> savedStatuses =  await statusRepository.GetStatuses();

            if (savedStatuses.Any())
            {
                foreach (var status in savedStatuses)
                {
                    if (status.geo != null && status.geo.coordinates != null && status.geo.coordinates.Any())
                        model.Coordinates.Add(status.geo.coordinates);
                }
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    string query = i == 0 ? "?q=%23surfsup" : nextUrl;

                    if (string.IsNullOrEmpty(query))
                        break;

                    StatusSearchResponse searchResponse = await TwitterApi.SearchStatus(query);

                    if (searchResponse != null && searchResponse.statuses != null && searchResponse.statuses.Any())
                    {
                        foreach (var status in searchResponse.statuses)
                        {
                            if (status.geo != null && status.geo.coordinates != null && status.geo.coordinates.Any())
                                model.Coordinates.Add(status.geo.coordinates);
                        }

                        await statusRepository.SaveStatuses(searchResponse.statuses);
                    }

                    nextUrl = searchResponse.search_metadata.next_results;
                }
            }

            timer.Stop();
            logger.LogInfo(Enums.LogType.Logging, string.Format("Exiting HomeController.Index in {0}", timer.ElapsedMilliseconds));

            return View(model);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
