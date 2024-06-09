using HouseRentingSystemFromFile.Core.Contracts.Statistic;
using HouseRentingSystemFromFile.Data.Data;
using HouseRentingSystemFromFile.Core.Models.Statistic;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemFromFile.Core.Services.Statistic
{
	public class StatisticService : IStatisticService
	{
		private readonly HouseRentingDbContext _data;

		public StatisticService(HouseRentingDbContext data)
		{
			_data = data;
		}

		public async Task<StatisticServiceModel> Total()
		{
			var totalHouses = await _data.Houses.CountAsync();
			var totalRents = await _data.Houses
				.Where(h => h.RenterId != null)
				.CountAsync();

			return new StatisticServiceModel()
			{
				TotalHouses = totalHouses,
				TotalRents = totalRents
			};
		}
	}
}
