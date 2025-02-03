namespace SeunOrderNotification.Models
{
	public class Order
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string CustomerName { get; set; } = string.Empty;
		public string CustomerEmail { get; set; } = string.Empty;
		public string CustomerPhone { get; set; } = string.Empty;
		public DateTime SessionDateTime { get; set; }
		public int SessionLengthMinutes { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
