using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Core.Contracts.House;
using HouseRentingSystemFromFile.Web.Areas.Admin.Models;
using HouseRentingSystemFromFile.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
    public class HouseController : AdminController
    {
        private readonly IHouseService _houses;
        private readonly IAgentService _agents;

        public HouseController(IHouseService houses, IAgentService agents)
        {
            _houses = houses;
            _agents = agents;
        }

        public async Task<IActionResult> Mine()
        {
            var myHouses = new MyHousesViewModel();

            var adminUserId = User.Id();
            myHouses.RentedHouses = await _houses.AllHousesByUserId(adminUserId);

            var adminAgentId = await _agents.GetAgentId(adminUserId);
            myHouses.AddedHouses = await _houses.AllHousesByAgentId(adminAgentId);

            return View(myHouses);
        }
    }
}
