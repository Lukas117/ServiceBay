using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBay.Dto;
using ServiceBay.Middleware;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcAuctionController : Controller
    {
        private readonly string uri = "https://localhost:44349/api/";

        
        public IActionResult Index()
        {
            IEnumerable<Auction> auction = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Auction>>();
                displaydata.Wait();

                auction = displaydata.Result;
            }
            return View(auction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            AuctionForCreationDto auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction/" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<AuctionForCreationDto>();
                displaydata.Wait();
                auction = displaydata.Result;
            }
            return View(auction);
        }

        [HttpGet]
        public IActionResult DetailsMyAuctions(int id)
        {
            AuctionForCreationDto auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction/" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<AuctionForCreationDto>();
                displaydata.Wait();
                auction = displaydata.Result;
            }
            return View(auction);
        }

        public IActionResult MyAuctions()
        {
            IEnumerable<Auction> auction = null;
          //  var currentUser = (Person)HttpContext.Items["User"];
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction/User");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Auction>>();
                displaydata.Wait();

                auction = displaydata.Result;
            }
            return View(auction);
        }

        [HttpPost]
        public IActionResult Create(AuctionForCreationDto inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var currentUser = (Person)HttpContext.Items["User"];
            inserttemp.SellerId = currentUser.Id;
            var insertrecord = hc.PostAsJsonAsync<AuctionForCreationDto>("ApiAuction", inserttemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAuctions");
            }
            return View("Create");
        }

        public JsonResult AllAuctions()
        {
            IEnumerable<Auction> auction = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Auction>>();
                displaydata.Wait();

                auction = displaydata.Result;
            }
            return Json(auction);
        }

        public JsonResult AllSellerAuctions(int sellerId)
         {
            IEnumerable<Auction> auction = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction/Seller/" + sellerId.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Auction>>();
                displaydata.Wait();

                auction = displaydata.Result;
            }
            return Json(auction);
        }

        public IActionResult Delete(int id)
        {
            Auction auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var readrecord = hc.GetAsync("ApiAuction/" + id.ToString());
            readrecord.Wait();

            var readdata = readrecord.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var readTask = readdata.Content.ReadAsAsync<Auction>();
                readTask.Wait();
                auction = readTask.Result;

            }
            return View(auction);
        }

        [HttpPost]
        public IActionResult Delete(Auction auction)
        {
            int id = auction.Id;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var removerecord = hc.DeleteAsync("ApiAuction/" + id.ToString());
            removerecord.Wait();

            var deletedata = removerecord.Result;
            if (deletedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        //[Authorize]
        public IActionResult Edit(int id)
        {
            Auction auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var readrecord = hc.GetAsync("ApiAuction/" + id.ToString());
            readrecord.Wait();

            var readdata = readrecord.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var readTask = readdata.Content.ReadAsAsync<Auction>();
                readTask.Wait();
                auction = readTask.Result;

            }
            return View(auction);
        }

        [HttpPost]
        public IActionResult Edit(int id, Auction auction)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var updaterecord = hc.PutAsJsonAsync<Auction>("ApiAuction/" + id.ToString(), auction);
            updaterecord.Wait();

            var updatadata = updaterecord.Result;
            if (updatadata.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAuctions");
            }
            return View(auction);
        }

        [HttpPost]
        public IActionResult DisableAuction(int id, Auction auction)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var disablerecord = hc.PutAsJsonAsync<Auction>("ApiAuction/Disable/" + id.ToString(), auction);
            disablerecord.Wait();

            var deletedata = disablerecord.Result;
            if (deletedata.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAuctions");
            }
            return View("MyAuctions");
        }

        [HttpPost]
        public JsonResult Disable(int id)
        {
            Auction auction = DetailsAuction(id);
            return Json(DisableAuction(id, auction));
        }
        
        [HttpGet]
        public Auction DetailsAuction(int id)
        {
            Auction auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiAuction/" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<Auction>();
                displaydata.Wait();
                auction = displaydata.Result;
            }
            return auction;
        }
    }
}