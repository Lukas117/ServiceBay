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
    public class MvcPersonController : Controller
    {
        private readonly string uri = "https://localhost:44349/api/";

        public IActionResult Index()
        {
            IEnumerable<Person> person = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiPerson");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Person>>();
                displaydata.Wait();

                person = displaydata.Result;
            }
            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CityForCreationDto cityDto, AddressForCreationDto addressDto,PersonForCreationDto inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var insertRecord1 = hc.PostAsJsonAsync<CityForCreationDto>("ApiCity", cityDto);
            insertRecord1.Wait();
            var insertRecord2 = hc.PostAsJsonAsync<AddressForCreationDto>("ApiAddress", addressDto);
            insertRecord2.Wait();
            var insertrecord = hc.PostAsJsonAsync<PersonForCreationDto>("ApiPerson", inserttemp);
            insertrecord.Wait();

            if (insertRecord1.Result.IsSuccessStatusCode && insertRecord2.Result.IsSuccessStatusCode && insertrecord.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Details(int id)
        {
            Person person = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);

            var consumeapi = hc.GetAsync("ApiPerson/" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<Person>();
                displaydata.Wait();
                person = displaydata.Result;
            }
            return View(person);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var removerecord = hc.DeleteAsync("ApiPerson/" + id.ToString());
            removerecord.Wait();

            var deletedata = removerecord.Result;
            if (deletedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPut]
        public IActionResult Edit(int id)
        {
            Person person = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var updaterecord = hc.GetAsync("ApiPerson/" + id.ToString());
            updaterecord.Wait();

            var updatadata = updaterecord.Result;
            if (updatadata.IsSuccessStatusCode)
            {
                var readTask = updatadata.Content.ReadAsAsync<Person>();
                readTask.Wait();
                person = readTask.Result;

            }

            return View(person);

        }
    }
}