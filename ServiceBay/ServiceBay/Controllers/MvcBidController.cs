using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBay.Dto;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcBidController : Controller
    {
        private readonly MvcAuctionController mvc;
        private readonly string uri = "https://localhost:44349/api/";

        public MvcBidController()
        {
            mvc = new MvcAuctionController();
        }

        public IActionResult Index()
        {
            IEnumerable<Bid> bid = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

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
            var user = StaticVar.currentUser;
            inserttemp.BuyerId = user.Id;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var getrecord = hc.GetAsync("ApiAuction/" + inserttemp.AuctionId.ToString());
            getrecord.Wait();
            string jsonString = getrecord.Result.Content.ReadAsStringAsync().Result;
            AuctionForCreationDto auction = JsonConvert.DeserializeObject<AuctionForCreationDto>(jsonString);
            if (user.Id == auction.SellerId) {
                StaticVar.error = 1;
                return View("~/Views/MvcAuction/Details.cshtml", auction);
            }

            var insertrecord = hc.PostAsJsonAsync<Bid>("ApiBid", inserttemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                StaticVar.error = 0;
                return RedirectToAction("Index");
            }
            StaticVar.error = 2;
            return View("~/Views/MvcAuction/Details.cshtml", auction);
        }

        [HttpPost]
        public JsonResult AllAuctions()
        {
            return Json(mvc.AllAuctions());
        }

        public IActionResult MyBids()
        {
            IEnumerable<Bid> bids = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiBid/User");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Bid>>();
                displaydata.Wait();

                bids = displaydata.Result;
            }
            return View(bids);
        }
    }
}