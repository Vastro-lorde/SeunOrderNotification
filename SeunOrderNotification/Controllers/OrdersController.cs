using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeunOrderNotification.Attributes;
using SeunOrderNotification.Models;
using SeunOrderNotification.Services;

namespace SeunOrderNotification.Controllers
{
	[JwtAuthorize]
	[Route("Dashboard/Orders")]
	public class OrdersController : Controller
	{
		private readonly ILogger<OrdersController> _logger;
		private readonly IOrderService _orderService;
		private readonly INotificationService _notificationService;

		public OrdersController(ILogger<OrdersController> logger, IOrderService orderService, INotificationService notificationService)
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
			var viewModel = new DashboardViewModel
			{
				UserEmail = userEmail,
				Orders = orders,
				Notifications = notifications
			};
			return View(viewModel);
		}

		[HttpGet("GetUserOrders")]
		public async Task<IActionResult> GetUserOrders()
		{
			string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
			string userId = HttpContext.Session.GetString("UserId") ?? "";
			var orders = await _orderService.GetOrdersByUserId(userId);
			return Ok(orders);
		}
	}
}
