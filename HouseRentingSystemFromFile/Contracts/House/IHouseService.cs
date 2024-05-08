using HouseRentingSystemFromFile.Models.House;

namespace HouseRentingSystemFromFile.Contracts.House
{
	public interface IHouseService
	{
		Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
	}
}
