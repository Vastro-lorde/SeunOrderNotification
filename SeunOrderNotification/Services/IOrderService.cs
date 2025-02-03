using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public interface IOrderService
	{
		void CreateOrder(Order order, string userId);
		Task<List<Order>> GetOrdersByUserId(string userId);
	}
}