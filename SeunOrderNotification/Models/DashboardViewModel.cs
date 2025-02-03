namespace SeunOrderNotification.Models
{
	public class DashboardViewModel
	{
		public string UserEmail { get; set; }
		public List<Order> Orders { get; set; }
		public List<Notification> Notifications { get; set; }
	}
}
