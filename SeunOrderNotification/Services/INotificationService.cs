using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public interface INotificationService
	{
		Task CreateNotificationAsync(Order order, string userId);
		Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
		Task MarkAsReadAsync(string notificationId);
	}
}