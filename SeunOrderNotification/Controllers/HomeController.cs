using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Models;
using SeunOrderNotification.Services;

namespace SeunOrderNotification.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;

		public HomeController(ILogger<HomeController> logger, IOrderService orderService, IUserService userService)
		{
			_logger = logger;
			_orderService = orderService;
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			var users = await _userService.GetUsers();
			if (users == null) 
			{
				return Error();
			}
			ViewBag.Users = users;
			return View();
		}
		[HttpGet]
		public IActionResult CreateOrder(string userId)
		{
			ViewBag.UserId = userId;
			return View();
		}

		[HttpPost]
		public IActionResult CreateNewOrder(OrderViewModel request)
		{
			string userId = Request.Form["UserId"];

			_orderService.CreateOrder(new Order
			{
				CreatedAt = DateTime.Now,
				CustomerEmail = request.CustomerEmail,
				CustomerPhone = request.CustomerPhone,
				CustomerName = request.CustomerName,
				SessionDateTime = request.SessionDateTime,
				SessionLengthMinutes = request.SessionLengthMinutes,
			}, userId);

			return RedirectToAction("Index", "Home");
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
