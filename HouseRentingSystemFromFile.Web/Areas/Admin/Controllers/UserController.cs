using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
	public class UserController : AdminController
	{
		private readonly IUserService _users;
		private readonly IMemoryCache _cache;

		public UserController(IUserService users, IMemoryCache cache)
		{
			_users = users;
			_cache = cache;
		}

		[Route("User/All")]
		public async Task<IActionResult> All()
		{
			var users = _cache.Get<IEnumerable<UserServiceModel>>(UsersCacheKey);


			if (users == null)
			{
				users = await _users.All();

				var cacheOptions = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

				_cache.Set(UsersCacheKey, users, cacheOptions);
			}

			return View(users);
		}
	}
}
