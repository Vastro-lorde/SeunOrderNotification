using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Models;

namespace SeunOrderNotification.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
		{
			string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
			var viewModel = new DashboardViewModel { UserEmail = userEmail };
			return View(viewModel);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
