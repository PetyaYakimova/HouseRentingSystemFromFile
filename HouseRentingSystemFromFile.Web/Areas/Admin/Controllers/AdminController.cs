using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
	[Area(AreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class AdminController : Controller
	{
	}
}
