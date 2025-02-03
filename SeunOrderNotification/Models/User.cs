﻿namespace SeunOrderNotification.Models
{
	public class User
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Email { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty; // In real application, this would be hashed
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
