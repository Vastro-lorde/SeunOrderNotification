using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public static class InMemoryStorage
	{
		private static readonly List<Order> Orders = [];
		private static readonly List<Notification> Notifications = [];
		private static readonly List<User> Users = [];

		static InMemoryStorage()
		{
			SeedUsers();
		}

		private static void SeedUsers()
		{
			Users.AddRange(
				[
					new User
			{
				Id = "photographer-1",
				Email = "alex.lens@example.com",
				Name = "Alex Lensworth",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-2",
				Email = "sophia.snaps@example.com",
				Name = "Sophia Snapwell",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-3",
				Email = "liam.focus@example.com",
				Name = "Liam Focus",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-4",
				Email = "olivia.frames@example.com",
				Name = "Olivia Frames",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-5",
				Email = "ethan.capture@example.com",
				Name = "Ethan Capture",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-6",
				Email = "mia.shutter@example.com",
				Name = "Mia Shutterfield",
				Password = "password123"
			},
			new User
			{
				Id = "photographer-7",
				Email = "noah.lights@example.com",
				Name = "Noah Lightman",
				Password = "password123"
			}
				]);
		}


		public static void AddNotification(Notification notification)
		{
			Notifications.Add(notification);
		}
		public static void RemoveNotification(string notificationId) 
		{
			Notification notification = Notifications.First(x => x.Id == notificationId);
			Notifications.Remove(notification);
		}

		public static void UpdateNotification(string notificationId, Notification notification)
		{
			Notifications.ForEach(x => {
				if (x.Id == notificationId)
				{
					x.OrderId = notification.OrderId;
					x.UserId = notification.UserId;
					x.IsRead = notification.IsRead;
					x.Order = notification.Order;
				}
			});
		}

		public static List<Notification> GetNotificationsByUserId(string userId)
		{
			return Notifications.FindAll(x => x.UserId == userId);
		}

		public static Notification GetNotificationById(string id) => Notifications.Find(x => x.Id == id);

		public static void CreateOrder(Order order)
		{
			Orders.Add(order);
		}

		public static void UpdateOrder(string  orderId, Order order)
		{
			Orders.ForEach(x =>
			{
				if (x.Id == orderId)
				{
					x.SessionLengthMinutes = order.SessionLengthMinutes;
					x.CustomerPhone = order.CustomerPhone;
					x.CustomerName = order.CustomerName;
					x.CustomerEmail = order.CustomerEmail;
					x.SessionDateTime = order.SessionDateTime;
				}
			});
		}

		public static Task<List<Order>> GetOrdersByUserId(string userId)
		{
			return Task.FromResult(Notifications
				.Where(n => n.UserId == userId)
				.Select(n => n.Order)
				.OrderByDescending(x => x.CreatedAt)
				.ToList());
		}


		public static User GetUserByEmail(string email)
		{
			return Users.Find(user => user.Email == email);
		}

		public static List<User> GetUsers()
		{
			return Users;
		}
	}
}
