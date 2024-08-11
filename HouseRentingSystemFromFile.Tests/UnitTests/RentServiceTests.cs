using HouseRentingSystemFromFile.Core.Contracts.Rent;
using HouseRentingSystemFromFile.Core.Services.Rent;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    [TestFixture]
    public class RentServiceTests : UnitTestsBase
    {
        private IRentService _rentService;

        [OneTimeSetUp]
        public void SetUp() => _rentService = new RentService(_data, _mapper);

        [Test]
        public async Task All_ShouldReturnCorrectData()
        {
            //Arrange

            //Act
            var result = await _rentService.All();

            //Assert
            Assert.IsNotNull(result);

            var rentedHousesInDb = _data.Houses.Where(h => h.RenterId != null);
            Assert.That(result.ToList().Count(), Is.EqualTo(rentedHousesInDb.Count()));

            var resultHouse = result.ToList()
                .Find(h => h.HouseTitle == RentedHouse.Title);
            Assert.IsNotNull(resultHouse);
            Assert.AreEqual(Renter.Email, resultHouse.RenterEmail);
            Assert.AreEqual(Renter.FirstName + " " + Renter.LastName, resultHouse.RenterFullName);
            Assert.AreEqual(Agent.User.Email, resultHouse.AgentEmail);
            Assert.AreEqual(Agent.User.FirstName + " " + Agent.User.LastName, resultHouse.AgentFullName);
        }
    }
}
