using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcAuctionController mvc;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            mvc = new MvcAuctionController();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(); 
        }

        [HttpPost]
        public JsonResult DetailsForNoti(int id)
        {
            return Json(mvc.DetailsForNoti(id));
        }
    }
}
