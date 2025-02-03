using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Attributes;
using SeunOrderNotification.Models;

namespace SeunOrderNotification.Controllers
{
	[JwtAuthorize]
	[Route("Dashboard/Orders")]
	public class OrdersController : Controller
	{
		private readonly ILogger<OrdersController> _logger;

		public OrdersController(ILogger<OrdersController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
			string userId = HttpContext.Session.GetString("UserId") ?? "";
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
