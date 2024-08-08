using HouseRentingSystemFromFile.Core.Contracts.Statistic;
using HouseRentingSystemFromFile.Core.Models.Statistic;
using Moq;

namespace HouseRentingSystemFromFile.Tests.Mocks
{
    public class StatisticsServiceMock
    {
        public static IStatisticService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .ReturnsAsync(new StatisticServiceModel()
                    {
                        TotalHouses = 10,
                        TotalRents = 6
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
