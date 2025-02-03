using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public class AuthService : IAuthService
	{
		public Task<User> LoginAsync(string email, string password)
		{
			// In a real application, you would hash the password before comparing
			var user = InMemoryStorage.GetUserByEmail(email);
			if (user != null && user.Password == password)
			{
				return Task.FromResult(user);
			}
			return Task.FromResult<User>(null);
		}
	}
}
