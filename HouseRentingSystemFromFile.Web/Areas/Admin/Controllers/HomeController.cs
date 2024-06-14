using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
	public class HomeController : AdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
