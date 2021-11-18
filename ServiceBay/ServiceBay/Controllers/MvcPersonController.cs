using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcPersonController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Person> person = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiPerson");

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
        public IActionResult Create(Person inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiPerson");

            var insertrecord = hc.PostAsJsonAsync<Person>("ApiPerson", inserttemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Details(int id)
        {
            Person person = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiPerson");

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
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiPerson");
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
            hc.BaseAddress = new Uri("https://localhost:5001/api/ApiPerson");
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