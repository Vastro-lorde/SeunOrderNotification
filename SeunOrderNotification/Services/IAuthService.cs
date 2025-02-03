using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public interface IAuthService
	{
		Task<User> LoginAsync(string email, string password);
	}
}