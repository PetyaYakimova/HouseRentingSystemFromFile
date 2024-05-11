using HouseRentingSystemFromFile.Contracts.Agent;
using HouseRentingSystemFromFile.Contracts.House;
using HouseRentingSystemFromFile.Infrastructure;
using HouseRentingSystemFromFile.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService _houses;
        private readonly IAgentService _agents;

        public HouseController(IHouseService houses, IAgentService agents)
        {
            _houses = houses;
            _agents = agents;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = _houses.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = await _houses.AllCategoriesNames();
            query.Categories = (IEnumerable<string>)houseCategories;

            return View(query);
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses = null;

            var userId = User.Id();

            if (await _agents.ExistsById(userId))
            {
                var currentAgentId = await _agents.GetAgentId(userId);

                myHouses = await _houses.AllHousesByAgentId(currentAgentId);
            }
            else
            {
                myHouses = await _houses.AllHousesByUserId(userId);
            }

            return View(myHouses);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await _houses.Exists(id) == false)
            {
                return BadRequest();
            }

            var houseModel = await _houses.HouseDetailsById(id);

            return View(houseModel);
        }

        public async Task<IActionResult> Add()
        {
            if (await _agents.ExistsById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            return View(new HouseFormModel()
            {
                Categories = await _houses.AllCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await _agents.ExistsById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await _houses.CategoryExists(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _houses.AllCategories();

                return View(model);
            }

            var agentId = await _agents.GetAgentId(User.Id());

            var newHouseId = await _houses.Create(model.Title, model.Address, model.Description, model.ImageUrl,
                model.PricePerMonth, model.CategoryId, agentId);

            return RedirectToAction(nameof(Details), new { id = newHouseId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (await _houses.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await _houses.HasAgentWithId(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var house = await _houses.HouseDetailsById(id);

            var houseCategoryId = await _houses.GetHouseCategoryId(house.Id);

            var houseModel = new HouseFormModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = houseCategoryId,
                Categories = await _houses.AllCategories()
            };

            return View(houseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            if (await _houses.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await _houses.HasAgentWithId(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await _houses.CategoryExists(house.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(house.CategoryId),
                    "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                house.Categories = await _houses.AllCategories();

                return View(house);
            }

            await _houses.Edit(id, house.Title, house.Address, house.Description, house.ImageUrl, house.PricePerMonth,
                house.CategoryId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _houses.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await _houses.HasAgentWithId(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var house = await _houses.HouseDetailsById(id);

            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel house)
        {
            if (await _houses.Exists(house.Id) == false)
            {
                return BadRequest();
            }

            if (await _houses.HasAgentWithId(house.Id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await _houses.Delete(house.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await _houses.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await _agents.ExistsById(User.Id()))
            {
                return Unauthorized();
            }

            if (await _houses.IsRented(id))
            {
                return BadRequest();
            }

            await _houses.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await _houses.Exists(id) == false ||
                await _houses.IsRented(id) == false)
            {
                return BadRequest();
            }

            if (await _houses.IsRentedByUserWithId(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await _houses.Leave(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
