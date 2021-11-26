using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBay.Dto;
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
            if(readdata.IsSuccessStatusCode)
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

        [HttpPost]
        public IActionResult Create(AuctionForCreationDto inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var insertrecord = hc.PostAsJsonAsync<AuctionForCreationDto>("ApiAuction", inserttemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public JsonResult DetailsForNoti(int id)
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
            return Json(auction.Price);
        }

        public JsonResult IsUpdated(int id)
        {
            bool result = false;
            var first = DetailsForNoti(id);
            System.Threading.Thread.Sleep(2000);
            var second = DetailsForNoti(id);
            if (first != null && second != null)
            {
                if (!first.Value.Equals(second.Value))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return Json(result);
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

        //public IActionResult Details(int id)
        //{
        //    AuctionForCreationDto auction = null;

        //    HttpClient hc = new HttpClient();
        //    hc.BaseAddress = new Uri(uri);

        //    var consumeapi = hc.GetAsync("ApiAuction/" + id.ToString());
        //    consumeapi.Wait();

        //    var readdata = consumeapi.Result;
        //    if (readdata.IsSuccessStatusCode)
        //    {
        //        var displaydata = readdata.Content.ReadAsAsync<AuctionForCreationDto>();
        //        displaydata.Wait();
        //        auction = displaydata.Result;
        //    }
        //    return View(auction);
        //}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
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

        public IActionResult Edit(int id)
        {
            AuctionForUpdateDto auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var readrecord = hc.GetAsync("ApiAuction/" + id.ToString());
            readrecord.Wait();

            var readdata = readrecord.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var readTask = readdata.Content.ReadAsAsync<AuctionForUpdateDto>();
                readTask.Wait();
                auction = readTask.Result;

            }
            return View(auction);
        }

        [HttpPost]
        public IActionResult Edit(int id, AuctionForUpdateDto auction)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var updaterecord = hc.PutAsJsonAsync<AuctionForUpdateDto>("ApiAuction/" + id.ToString(), auction);
            updaterecord.Wait();

            var updatadata = updaterecord.Result;
            if (updatadata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(auction);
        }

        [HttpPut]
        public IActionResult Disable(int id, Auction auction)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var disablerecord = hc.PutAsJsonAsync<Auction>("ApiAuction/disable/" + id.ToString(), auction);
            disablerecord.Wait();

            var deletedata = disablerecord.Result;
            if (deletedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public JsonResult DisableAuction(int id, Auction auction)
        {
            return Json(Disable(id, auction));
        }
        
        [HttpGet]
        public JsonResult DetailsAuction(int id)
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
            return Json(auction);
        }
    }
}