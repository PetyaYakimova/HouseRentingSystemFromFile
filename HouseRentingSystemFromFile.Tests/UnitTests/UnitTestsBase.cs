using AutoMapper;
using HouseRentingSystemFromFile.Data.Data;
using HouseRentingSystemFromFile.Data.Data.Models;
using HouseRentingSystemFromFile.Tests.Mocks;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected HouseRentingDbContext _data;
        protected IMapper _mapper;

        public ApplicationUser Renter { get; private set; }

        public Agent Agent { get; private set; }

        public House RentedHouse { get; private set; }

        private void SeedDatabase()
        {
            Renter = new ApplicationUser()
            {
                Id = "RenterUserId",
                Email = "rent@er.bg",
                FirstName = "Renter",
                LastName = "User"
            };
            _data.Users.Add(Renter);

            Agent = new Agent()
            {
                PhoneNumber = "+3591111111",
                User = new ApplicationUser()
                {
                    Id = "TestUserId",
                    Email = "test@test.bg",
                    FirstName = "Test",
                    LastName = "Tester"
                }
            };
            _data.Agents.Add(Agent);

            RentedHouse = new House()
            {
                Title = "First Test house",
                Address = "Test, 201 Test",
                Description = "This is a test description. This is a test desctiption. This is a test description.",
                ImageUrl = "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
                Renter = Renter,
                Agent = Agent,
                Category = new Category() { Name = "Cottage" }
            };
            _data.Houses.Add(RentedHouse);

            var notRentedHouse = new House()
            {
                Title = "Second Test house",
                Address = "Test, 204 Test",
                Description = "This is another test description. This is a test desctiption. This is a test description.",
                ImageUrl = "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
                Renter = null,
                Agent = Agent,
                Category = new Category() { Name = "Single-family" }
            };
            _data.Houses.Add(notRentedHouse);
            _data.SaveChanges();
        }

        [OneTimeSetUp]
        public void SetUpBase()
        {
            _data = DatabaseMock.Instance;
            _mapper = MapperMock.Instance;
            SeedDatabase();
        }

        [OneTimeTearDown]
        public void TearDownBase() => _data.Dispose();
    }
}
