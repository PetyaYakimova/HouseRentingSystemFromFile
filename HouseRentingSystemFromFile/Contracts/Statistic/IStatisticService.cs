using HouseRentingSystemFromFile.Models.Statistic;

namespace HouseRentingSystemFromFile.Contracts.Statistic
{
	public interface IStatisticService
	{
		Task<StatisticServiceModel> Total();
	}
}
