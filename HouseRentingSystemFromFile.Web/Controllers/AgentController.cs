﻿using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Web.Models.Agent;
using HouseRentingSystemFromFile.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Web.Controllers
{
	[Authorize]
	public class AgentController : Controller
    {
        private readonly IAgentService _agents;

        public AgentController(IAgentService agents)
        {
			_agents = agents;
        }

        public async Task<IActionResult> Become()
		{
            if (await _agents.ExistsById(User.Id()))
            {
                return BadRequest();
            }

            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (await _agents.ExistsById(userId))
            {
                return BadRequest();
            }

            if (await _agents.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exists. Enter another one.");
            }

            if (await _agents.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", 
                    "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _agents.Create(userId, model.PhoneNumber);

            TempData["message"] = "You have successfully become an agent";

            return RedirectToAction(nameof(HouseController.All), "House");
		}
	}
}
