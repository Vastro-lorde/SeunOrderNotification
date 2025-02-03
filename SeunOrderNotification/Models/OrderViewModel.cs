namespace SeunOrderNotification.Models
{
	public class OrderViewModel
	{
		public string CustomerName { get; set; } = string.Empty;
		public string CustomerEmail { get; set; } = string.Empty;
		public string CustomerPhone { get; set; } = string.Empty;
		public DateTime SessionDateTime { get; set; }
		public int SessionLengthMinutes { get; set; }
	}
}
