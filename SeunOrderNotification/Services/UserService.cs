using Microsoft.Extensions.Caching.Memory;
using SeunOrderNotification.Models;

namespace SeunOrderNotification.Services
{
	public class UserService : IUserService
	{
		private readonly IMemoryCache _cache;
		public UserService(IMemoryCache cache)
		{
			_cache = cache;
		}

		public async Task<List<User>> GetUsers(string username, string password)
		{
			return await _cache.GetOrCreateAsync(
				$"allusers",
				async entry =>
				{
					entry.SlidingExpiration = TimeSpan.FromMinutes(10);
					return InMemoryStorage.GetUsers();
				});
		}
	}
}
