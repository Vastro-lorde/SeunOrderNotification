using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SeunOrderNotification.Models;
using SeunOrderNotification.Services.Hub;

namespace SeunOrderNotification.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IMemoryCache _cache;
		private readonly IHubContext<NotificationHub> _hubContext;

		public NotificationService(
			IMemoryCache cache,
			IHubContext<NotificationHub> hubContext)
		{
			_cache = cache;
			_hubContext = hubContext;
		}

		public async Task CreateNotificationAsync(Order order, string userId)
		{
			var notification = new Notification
			{
				OrderId = order.Id,
				UserId = userId,
				IsRead = false,
				Order = order,
			};

			InMemoryStorage.AddNotification(notification);

			// Clear cache
			_cache.Remove($"unread_notifications_{userId}");

			// Send real-time notification
			await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification",
				$"New booking from {order.CustomerName} for {order.SessionDateTime:MMM dd, yyyy HH:mm}");
		}

		public async Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
		{
			return await _cache.GetOrCreateAsync(
				$"unread_notifications_{userId}",
				async entry =>
				{
					entry.SlidingExpiration = TimeSpan.FromMinutes(10);
					return InMemoryStorage.GetNotificationsByUserId(userId).Where(n => n.UserId == userId && !n.IsRead)
						.OrderByDescending(n => n.CreatedAt).ToList();
				});
		}

		public async Task MarkAsReadAsync(string notificationId)
		{
			var notification = InMemoryStorage.GetNotificationById(notificationId);
			notification.IsRead = true;
			InMemoryStorage.UpdateNotification(notificationId, notification);

			_cache.Remove($"unread_notifications_{notification.UserId}");
			await Task.CompletedTask;
		}
	}
}
