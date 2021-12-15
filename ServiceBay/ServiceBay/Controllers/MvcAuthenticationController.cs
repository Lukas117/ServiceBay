using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcAuthenticationController : Controller
    {
        private readonly string uri = "https://localhost:44349/api/";

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

            var response = hc.PostAsJsonAsync<Login>("ApiAuthentication/UserLogin", login);
            response.Wait();


            var saved = response.Result;
            if (saved.IsSuccessStatusCode)
            {
                var tokenbased = response.Result.Content.ReadAsStringAsync().Result;
                //string tokenbased = JsonConvert.DeserializeObject<string>(responseMessage);
                HttpContext.Session.SetString("Token", tokenbased);
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError(nameof(login.Password), "Wrong email or password");
            return View();
        }
    }
}