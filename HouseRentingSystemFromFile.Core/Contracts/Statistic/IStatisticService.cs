using HouseRentingSystemFromFile.Core.Models.Statistic;

namespace HouseRentingSystemFromFile.Core.Contracts.Statistic
{
	public interface IStatisticService
	{
		Task<StatisticServiceModel> Total();
	}
}
