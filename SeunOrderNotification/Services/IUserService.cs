using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public interface IUserService
	{
		Task<List<User>> GetUsers(string username, string password);
	}
}