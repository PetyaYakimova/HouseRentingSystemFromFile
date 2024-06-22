using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Contracts.House;
using HouseRentingSystemFromFile.Core.Services.ApplicationUser;
using HouseRentingSystemFromFile.Core.Services.House;
using HouseRentingSystemFromFile.Data.Data.Models;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    [TestFixture]
    public class HouseServiceTests : UnitTestsBase
    {
        private IUserService _userService;
        private IHouseService _houseService;

        [OneTimeSetUp]
        public void SetUp()
        {
            this._userService = new UserService(this._data, this._mapper);
            this._houseService = new HouseService(this._data, this._userService, this._mapper);
        }

        [Test]
        public async Task AllCategoryName_ShouldReturnCorrectResult()
        {
            //Arrange

            //Act
            var result = await _houseService.AllCategoriesNames();

            //Assert
            var dbCategories = _data.Categories;
            Assert.That(result.Count(), Is.EqualTo(dbCategories.Count()));

            var categoryNames = dbCategories.Select(c => c.Name);
            Assert.That(categoryNames.Contains(result.FirstOrDefault()));
        }

        [Test]
        public async Task AllHousesByAgentId_ShouldReturnCorrectHouses()
        {
            //Arrange
            var agentId = Agent.Id;

            //Act
            var result = await _houseService.AllHousesByAgentId(agentId);

            //Assert
            Assert.IsNotNull(result);

            var housesInDb = _data.Houses.Where(h => h.AgentId == agentId);
            Assert.That(result.Count(), Is.EqualTo(housesInDb.Count()));
        }

        [Test]
        public async Task AllHousesbyUserId_ShouldReturnCorrectHouses()
        {
            //Arrange
            var renterId = Renter.Id;

            //Act
            var result = await _houseService.AllHousesByUserId(renterId);

            //Assert
            Assert.IsNotNull(result);

            var housesInDb = _data.Houses.Where(h => h.RenterId == renterId);
            Assert.That(result.Count(), Is.EqualTo(housesInDb.Count()));
        }

        [Test]
        public async Task Exists_ShouldReturnCorrectTrue_WithValidId()
        {
            //Arrange
            var houseId = RentedHouse.Id;

            //Act
            var result = await _houseService.Exists(houseId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task HouseDetailsById_ShouldReturnCorrectHouseData()
        {
            //Arrange
            var houseId = RentedHouse.Id;

            //Act
            var result = await _houseService.HouseDetailsById(houseId);

            //Assert
            Assert.IsNotNull(result);

            var houseInDb = _data.Houses.Find(houseId);
            Assert.That(result.Id, Is.EqualTo(houseInDb.Id));
            Assert.That(result.Title, Is.EqualTo(houseInDb.Title));
        }

        [Test]
        public async Task AllCategories_ShouldReturnCorrectCategories()
        {
            //Arrange

            //Act
            var result = await _houseService.AllCategories();

            //Assert
            var dbCategories = _data.Categories;
            Assert.That(result.Count(), Is.EqualTo(dbCategories.Count()));

            var categoryNames = dbCategories.Select(c => c.Name);
            Assert.That(categoryNames.Contains(result.FirstOrDefault().Name));
        }

        [Test]
        public async Task Create_ShouldCreateHouse()
        {
            //Arrange
            var housesInDbBefore = _data.Houses.Count();
            var newHouse = new House()
            {
                Title = "New House",
                Address = "Test, 201 Test",
                Description = "This is a test description. This is a test desctiption. This is a test description.",
                ImageUrl = "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg"
            };

            //Act
            var newHouseId = await _houseService.Create(newHouse.Title, newHouse.Address, newHouse.Description, newHouse.ImageUrl, 2200.00M, 1, Agent.Id);

            //Assert
            var housesInDbAfter = _data.Houses.Count();
            Assert.That(housesInDbAfter, Is.EqualTo(housesInDbBefore + 1));

            var newHouseInDb = _data.Houses.Find(newHouseId);
            Assert.That(newHouseInDb.Title, Is.EqualTo(newHouse.Title));
        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnTrue_WithValidId()
        {
            //Arrange
            var houseId = RentedHouse.Id;
            var userId = RentedHouse.Agent.User.Id;

            //Act
            var result = await _houseService.HasAgentWithId(houseId, userId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Edit_ShouldEditHouseCorrectly()
        {
            //Arrange 
            var house = new House()
            {
                Title = "New House For Edit",
                Address = "Test, 201 Test",
                Description = "This is a test description. This is a test desctiption. This is a test description.",
                ImageUrl = "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg"
            };

            await _data.Houses.AddAsync(house);
            await _data.SaveChangesAsync();

            var changedAddress = "Sofia, Bulgaria";

            //Act
            await _houseService.Edit(house.Id, house.Title, changedAddress, house.Description, house.ImageUrl, house.PricePerMonth, house.CategoryId);

            //Assert
            var newHouseInDb = await _data.Houses.FindAsync(house.Id);
            Assert.IsNotNull(newHouseInDb);
            Assert.That(newHouseInDb.Title, Is.EqualTo(house.Title));
            Assert.That(newHouseInDb.Address, Is.EqualTo(changedAddress));
        }
    }
}
