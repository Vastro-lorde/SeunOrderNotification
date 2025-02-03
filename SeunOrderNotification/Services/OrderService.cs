using Microsoft.Extensions.Caching.Memory;
using SeunOrderNotification.Models;
using System.Runtime.InteropServices;

namespace SeunOrderNotification.Services
{
	public class OrderService
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

		}

		public async Task<List<Order>> GetOrdersByUserId(string userId)
		{
			return await _cache.GetOrCreateAsync(
				$"{userId}",
				async entry =>
				{
					entry.SlidingExpiration = TimeSpan.FromMinutes(10);
					return InMemoryStorage.GetOrdersByUserId(userId);
				});
		}
	}
}
