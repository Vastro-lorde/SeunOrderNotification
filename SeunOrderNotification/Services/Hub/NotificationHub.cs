using Microsoft.AspNetCore.SignalR;
namespace SeunOrderNotification.Services.Hub
{
	public class NotificationHub : DynamicHub
	{
		public async Task SendNotification(int photographerId, string message)
		{
			await Clients.User(photographerId.ToString()).SendAsync("ReceiveNotification", message);
		}
	}
}
