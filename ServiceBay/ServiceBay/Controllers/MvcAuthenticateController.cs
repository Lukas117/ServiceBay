using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class MvcAuthenticateController : Controller
    {

        private readonly string uri = "https://localhost:5001/api/";

        public IActionResult Index()
        {
            return View();
        }

       
    }
}