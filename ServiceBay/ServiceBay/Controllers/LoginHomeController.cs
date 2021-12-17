﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceBay.Controllers
{
    public class LoginHomeController : Controller
    {
        private readonly ILogger<LoginHomeController> _logger;

        public LoginHomeController(ILogger<LoginHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
