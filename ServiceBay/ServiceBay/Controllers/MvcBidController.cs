using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcBidController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Bid> bid = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/ApiBid");

            var consumeapi = hc.GetAsync("ApiBid");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Bid>>();
                displaydata.Wait();

                bid = displaydata.Result;
            }
            return View(bid);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bid inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/ApiBid");

            var insertrecord = hc.PostAsJsonAsync<Bid>("ApiBid", inserttemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }
    }
}