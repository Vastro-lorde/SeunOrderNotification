using Microsoft.Extensions.Caching.Memory;
using SeunOrderNotification.Models;
using System.Runtime.InteropServices;

namespace SeunOrderNotification.Services
{
	public class OrderService : IOrderService
	{
		private readonly INotificationService _notificationService;
		private readonly IMemoryCache _cache;
		public OrderService(IMemoryCache cache, INotificationService notificationService)
		{
			_notificationService = notificationService;
			_cache = cache;
		}

		public async void CreateOrder(Order order, string userId)
		{
			InMemoryStorage.CreateOrder(order);
			await _notificationService.CreateNotificationAsync(order, userId);
			var updateOrders = await InMemoryStorage.GetOrdersByUserId(userId);
			_cache.Set($"{userId}orders", updateOrders);

		}

		public async Task<List<Order>> GetOrdersByUserId(string userId)
		{
			var data = _cache.Get<List<Order>>($"{userId}orders");
			if (data == null) 
			{
				var newData = await InMemoryStorage.GetOrdersByUserId(userId);
				_cache.Set($"{userId}orders", newData);
				return newData;
			}
			return data;
		}
	}
}
