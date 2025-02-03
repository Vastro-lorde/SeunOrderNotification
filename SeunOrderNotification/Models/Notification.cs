namespace SeunOrderNotification.Models
{
	public class Notification
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string OrderId { get; set; } = Guid.NewGuid().ToString();
		public required Order Order { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string UserId { get; set; } = string.Empty;  // Photographer's ID
	}
}
