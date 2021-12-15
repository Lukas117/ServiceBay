using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBay.Models;
using System.Web;
using System.Net;
using System.Security.Claims;

namespace ServiceBay.Controllers
{
    public class MvcAuthenticationController : Controller
    {
        private readonly string uri = "https://localhost:5001/api/";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(uri);
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = hc.PostAsJsonAsync<Login>("ApiAuthentication/UserLogin", login);
            response.Wait();


            var saved = response.Result;
            if (saved.IsSuccessStatusCode)
            {
                var responseMessage = response.Result.Content.ReadAsStringAsync().Result;
                string tokenbased = JsonConvert.DeserializeObject<string>(responseMessage);
                HttpContext.Session.SetString("Token", tokenbased);
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}