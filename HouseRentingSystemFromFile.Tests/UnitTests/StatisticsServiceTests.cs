using HouseRentingSystemFromFile.Core.Contracts.Statistic;
using HouseRentingSystemFromFile.Core.Services.Statistic;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    [TestFixture]
    public class StatisticsServiceTests : UnitTestsBase
    {
        private IStatisticService _statisticService;

        [OneTimeSetUp]
        public void SetUp() => _statisticService = new StatisticService(_data);

        [Test]
        public async Task Total_ShouldReturnCorrectCounts()
        {
            //Arrange

            //Act
            var result = await _statisticService.Total();

            //Assert
            Assert.IsNotNull(result);

            var housesCount = _data.Houses.Count();
            Assert.That(result.TotalHouses, Is.EqualTo(housesCount));

            var rentsCount = _data.Houses.Where(h => h.RenterId != null).Count();
            Assert.That(result.TotalRents, Is.EqualTo(rentsCount));
        }
    }
}
