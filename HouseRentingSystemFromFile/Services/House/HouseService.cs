using HouseRentingSystemFromFile.Contracts.House;
using HouseRentingSystemFromFile.Data;
using HouseRentingSystemFromFile.Models.House;

namespace HouseRentingSystemFromFile.Services.House
{
	public class HouseService : IHouseService
	{
		private readonly HouseRentingDbContext _data;

		public HouseService(HouseRentingDbContext data)
		{
			_data = data;
		}

		public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
		{
			return _data
				.Houses
				.OrderByDescending(c => c.Id)
				.Select(c => new HouseIndexServiceModel()
				{
					Id = c.Id,
					Title = c.Title,
					ImageUrl = c.ImageUrl
				})
				.Take(3);
		}
	}
}
