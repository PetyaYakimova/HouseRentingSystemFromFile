using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Core.Contracts.House;
using HouseRentingSystemFromFile.Core.Models.House;
using HouseRentingSystemFromFile.Web.Models.House;
using HouseRentingSystemFromFile.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRentingSystemFromFile.Web.Controllers
{
	[Authorize]
	public class HouseController : Controller
	{
		private readonly IHouseService _houses;
		private readonly IAgentService _agents;
		private readonly IMapper _mapper;
		private readonly IMemoryCache _cache;

		public HouseController(IHouseService houses, IAgentService agents, IMapper mapper, IMemoryCache cache)
		{
			_houses = houses;
			_agents = agents;
			_mapper = mapper;
			_cache = cache;
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
			if (User.IsInRole(AdminRoleName))
			{
				return RedirectToAction(actionName: "mine", controllerName: "House", new { area = AreaName });
			}
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

		public async Task<IActionResult> Details(int id, string information)
		{
			if (await _houses.Exists(id) == false)
			{
				return BadRequest();
			}

			var houseModel = await _houses.HouseDetailsById(id);

			if (information != houseModel.GetInformation())
			{
				return BadRequest();
			}

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

            TempData["message"] = "You have successfully added a house!";

            return RedirectToAction(nameof(Details), new { id = newHouseId, information = model.GetInformation() });
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (await _houses.Exists(id) == false)
			{
				return BadRequest();
			}

			if (await _houses.HasAgentWithId(id, User.Id()) == false
				&& User.IsAdmin() == false)
			{
				return Unauthorized();
			}

			var house = await _houses.HouseDetailsById(id);

			var houseCategoryId = await _houses.GetHouseCategoryId(house.Id);

			var houseModel = _mapper.Map<HouseFormModel>(house);
			houseModel.CategoryId = houseCategoryId;
			houseModel.Categories = await _houses.AllCategories();

			return View(houseModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, HouseFormModel house)
		{
			if (await _houses.Exists(id) == false)
			{
				return BadRequest();
			}

			if (await _houses.HasAgentWithId(id, User.Id()) == false
				&& User.IsAdmin() == false)
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

            TempData["message"] = "You have successfully edited a house!";

            return RedirectToAction(nameof(Details), new { id = id, information = house.GetInformation() });
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (await _houses.Exists(id) == false)
			{
				return BadRequest();
			}

			if (await _houses.HasAgentWithId(id, User.Id()) == false
				&& User.IsAdmin() == false)
			{
				return Unauthorized();
			}

			var house = await _houses.HouseDetailsById(id);

			var model = _mapper.Map<HouseDetailsViewModel>(house);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(HouseDetailsViewModel house)
		{
			if (await _houses.Exists(house.Id) == false)
			{
				return BadRequest();
			}

			if (await _houses.HasAgentWithId(house.Id, User.Id()) == false
				&& User.IsAdmin() == false)
			{
				return Unauthorized();
			}

			await _houses.Delete(house.Id);

            TempData["message"] = "You have successfully deleted a house!";

            return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Rent(int id)
		{
			if (await _houses.Exists(id) == false)
			{
				return BadRequest();
			}

			if (await _agents.ExistsById(User.Id())
				&& User.IsAdmin() == false)
			{
				return Unauthorized();
			}

			if (await _houses.IsRented(id))
			{
				return BadRequest();
			}

			await _houses.Rent(id, User.Id());

			_cache.Remove(RentsCacheKey);

            TempData["message"] = "You have successfully rented a house!";

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

			_cache.Remove(RentsCacheKey);

            TempData["message"] = "You have successfully left a house!";

            return RedirectToAction(nameof(Mine));
		}
	}
}
