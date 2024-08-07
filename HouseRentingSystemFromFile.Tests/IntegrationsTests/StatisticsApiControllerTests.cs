using HouseRentingSystemFromFile.Tests.Mocks;
using HouseRentingSystemFromFile.Web.Controllers.Api;

namespace HouseRentingSystemFromFile.Tests.IntegrationsTests
{
    [TestFixture]
    public class StatisticsApiControllerTests
    {
        private StatisticApiController statisticsApiController;

        [OneTimeSetUp]
        public void SetUp() => this.statisticsApiController = new StatisticApiController(StatisticsServiceMock.Instance);

        [Test]
        public async Task GetStatistics_ShouldReturnCorrectCounts()
        {
            //Arrange

            //Act
            var result = await this.statisticsApiController.GetStatistic();

            //Assert
            Assert.NotNull(result);
            Assert.That(result.TotalHouses, Is.EqualTo(10));
            Assert.That(result.TotalRents, Is.EqualTo(6));
        }
    }
}
