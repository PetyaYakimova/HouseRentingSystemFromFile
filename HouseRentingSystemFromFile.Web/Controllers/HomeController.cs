using Microsoft.AspNetCore.Mvc;
using HouseRentingSystemFromFile.Core.Contracts.House;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService _houses;

        public HomeController(IHouseService houses)
        {
            _houses = houses;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = AreaName });
            }
            var houses = await _houses.LastThreeHouses();
            return View(houses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}