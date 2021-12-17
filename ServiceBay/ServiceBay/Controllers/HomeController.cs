using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceBay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NotificationController noti;
        private readonly MvcPersonController personController;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            noti = new NotificationController();
            personController = new MvcPersonController();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SetList()
        {
            return Json(noti.SetPrevPrices());
        }

        [HttpGet]
        public JsonResult PricesForNotification()
        {
            return Json(noti.CompareAndGetPrices());
        }

    }
}
