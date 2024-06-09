using HouseRentingSystemFromFile.Core.Contracts.Statistic;
using HouseRentingSystemFromFile.Core.Models.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Web.Controllers.Api
{
	[ApiController]
	[Route("api/statistic")]
	public class StatisticApiController : ControllerBase
	{
		private readonly IStatisticService _statistics;

		public StatisticApiController(IStatisticService statistics)
		{
			_statistics = statistics;
		}

		[HttpGet]
		public async Task<StatisticServiceModel> GetStatistic()
		{
			return await _statistics.Total();
		}
	}
}
