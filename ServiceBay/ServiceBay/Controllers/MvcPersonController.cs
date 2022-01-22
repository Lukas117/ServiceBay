using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBay.Dto;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcPersonController : Controller
    {
        private readonly string uri = "https://localhost:5001/api/";

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
        public IActionResult Create(PersonViewModel inserttemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var insertrecord = hc.PostAsJsonAsync<CityForCreationDto>("ApiCity", inserttemp.cityDto);
            insertrecord.Wait();
            inserttemp.addressDto.CityZipcode = inserttemp.cityDto.Zipcode;
            var insertrecord1 = hc.PostAsJsonAsync<AddressForCreationDto>("ApiAddress", inserttemp.addressDto);
            insertrecord1.Wait();
            string jsonString = insertrecord1.Result.Content.ReadAsStringAsync().Result;
            AddressForCreationDto returnedAddress = JsonConvert.DeserializeObject<AddressForCreationDto>(jsonString);
            inserttemp.personDto.AddressId = returnedAddress.Id;
            inserttemp.personDto.UserRole = 0;
            var insertrecord2 = hc.PostAsJsonAsync<PersonForCreationDto>("ApiPerson", inserttemp.personDto);
            insertrecord2.Wait();
            if (!insertrecord2.Result.IsSuccessStatusCode) { ModelState.AddModelError("personDto.Email", "The user with this email already exists"); }
            if (insertrecord.Result.IsSuccessStatusCode && insertrecord1.Result.IsSuccessStatusCode && insertrecord2.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","LoginHome");
            }
            return View("Create");
        }

        public IActionResult Details()
        {
            PersonViewModel personView = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var currentUser = StaticVar.currentUser;

            var consumeapi = hc.GetAsync("ApiPerson/" + currentUser.Id.ToString());
            consumeapi.Wait();
            var consumeapi1 = hc.GetAsync("ApiAddress/" + currentUser.AddressId.ToString());
            consumeapi1.Wait();
            var read = consumeapi1.Result.Content.ReadAsAsync<AddressForCreationDto>();
            var zipcode = read.Result.CityZipcode;
            var consumeapi2 = hc.GetAsync("ApiCity/" + zipcode.ToString());
            consumeapi2.Wait();

            if (consumeapi.Result.IsSuccessStatusCode && consumeapi1.Result.IsSuccessStatusCode && consumeapi2.Result.IsSuccessStatusCode)
            {
                var displaydata = consumeapi.Result.Content.ReadAsAsync<PersonForCreationDto>();
                displaydata.Wait();
                var displaydata2 = consumeapi2.Result.Content.ReadAsAsync<CityForCreationDto>();
                displaydata2.Wait();
                PersonForCreationDto person = displaydata.Result;
                AddressForCreationDto address = read.Result;
                CityForCreationDto city = displaydata2.Result;
                personView = new() { personDto = person, addressDto = address, cityDto = city };
            }
            return View(personView);
        }

        public IActionResult Edit(int id)
        {
            PersonViewModel personView = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var currentUser = (Person)HttpContext.Items["User"];

            var consumeapi = hc.GetAsync("ApiPerson/" + currentUser.Id.ToString());
            consumeapi.Wait();
            var consumeapi1 = hc.GetAsync("ApiAddress/" + currentUser.AddressId.ToString());
            consumeapi1.Wait();
            var read = consumeapi1.Result.Content.ReadAsAsync<AddressForCreationDto>();
            var zipcode = read.Result.CityZipcode;
            var consumeapi2 = hc.GetAsync("ApiCity/" + zipcode.ToString());
            consumeapi2.Wait();

            if (consumeapi.Result.IsSuccessStatusCode && consumeapi1.Result.IsSuccessStatusCode && consumeapi2.Result.IsSuccessStatusCode)
            {
                var displaydata = consumeapi.Result.Content.ReadAsAsync<PersonForCreationDto>();
                displaydata.Wait();
                var displaydata2 = consumeapi2.Result.Content.ReadAsAsync<CityForCreationDto>();
                displaydata2.Wait();
                PersonForCreationDto person = displaydata.Result;
                AddressForCreationDto address = read.Result;
                CityForCreationDto city = displaydata2.Result;
                personView = new() { personDto = person, addressDto = address, cityDto = city };
            }
            return View(personView);
        }

        [HttpPost]
        public IActionResult Edit(int id, PersonViewModel personView)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            var updaterecord = hc.PutAsJsonAsync<CityForCreationDto>("ApiCity/" + personView.cityDto.Zipcode.ToString(), personView.cityDto);
            updaterecord.Wait();
            personView.addressDto.CityZipcode = personView.cityDto.Zipcode;
            var updaterecord1 = hc.PutAsJsonAsync<AddressForCreationDto>("ApiAddress/" + personView.addressDto.Id.ToString(), personView.addressDto);
            updaterecord1.Wait();
            personView.personDto.AddressId = personView.addressDto.Id;
            var updaterecord2 = hc.PutAsJsonAsync<PersonForCreationDto>("ApiPerson/" + id.ToString(), personView.personDto);
            updaterecord2.Wait();

            if (updaterecord.Result.IsSuccessStatusCode && updaterecord1.Result.IsSuccessStatusCode && updaterecord2.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Details");
            }
            return View(personView);
        }
    }
}