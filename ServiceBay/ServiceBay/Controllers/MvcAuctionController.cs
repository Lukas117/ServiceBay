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
        public IActionResult Index()
        {
            IEnumerable<Auction> auction = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");

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
        public IActionResult Create(Auction inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");

            var insertrecord = hc.PostAsJsonAsync<Auction>("ApiAuction", inserttemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        
        public IActionResult Details(int id)
        {
            AuctionForCreationDto auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");

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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");
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
            Auction auction = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");
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

        [HttpPut]
        public IActionResult Edit(Auction auction)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiAuction");
            var updaterecord = hc.PutAsJsonAsync<Auction>("ApiAuction", auction);
            updaterecord.Wait();

            var updatadata = updaterecord.Result;
            if (updatadata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(auction);
        }

        //public IActionResult Disable(int id)
        //{
        //    HttpClient hc = new HttpClient();
        //    hc.BaseAddress = new Uri("https://localhost:44349/api/ApiAuction");
        //    var disablerecord = hc.GetAsync("ApiAuction/" + id.ToString());
        //    disablerecord.Wait();

        //    var deletedata = disablerecord.Result;
        //    if (deletedata.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View("Index");
        //}
    }
}