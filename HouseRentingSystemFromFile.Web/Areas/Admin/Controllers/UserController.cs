using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
	public class UserController : AdminController
	{
		private readonly IApplicationUserService _users;

		public UserController(IApplicationUserService users)
		{
			_users = users;
		}

		[Route("User/All")]
		public async Task<IActionResult> All()
		{
			var users = await _users.All();
			return View(users);
		}
	}
}
