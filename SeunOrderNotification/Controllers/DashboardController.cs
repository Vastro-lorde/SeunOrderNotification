using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Attributes;
using SeunOrderNotification.Models;
using SeunOrderNotification.Services;

namespace SeunOrderNotification.Controllers
{
	[JwtAuthorize]
	public class DashboardController : Controller
	{
		private readonly ILogger<DashboardController> _logger;
		private readonly IOrderService _orderService;
		private readonly INotificationService _notificationService;

		public DashboardController(ILogger<DashboardController> logger, IOrderService orderService, INotificationService notificationService)
		{
			_logger = logger;
			_orderService = orderService;
			_notificationService = notificationService;
		}

		public async Task<IActionResult> Index()
		{
			string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
			string userId = HttpContext.Session.GetString("UserId") ?? "";
			var orders = await _orderService.GetOrdersByUserId(userId);
			var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
			var viewModel = new DashboardViewModel { 
				UserEmail = userEmail, 
				Orders = orders, 
				Notifications = notifications 
			};
			return View(viewModel);
		}

		public async Task<IActionResult> Notifications()
		{
			string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
			string userId = HttpContext.Session.GetString("UserId") ?? "";
			var orders = await _orderService.GetOrdersByUserId(userId);
			var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
			var viewModel = new DashboardViewModel
			{
				UserEmail = userEmail,
				Orders = orders,
				Notifications = notifications
			};
			return View(viewModel);
		}
		[HttpGet]
		public async Task<IActionResult> GetNotifications()
		{
			string userId = HttpContext.Session.GetString("UserId") ?? "";
			var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
			
			return Ok(notifications);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
