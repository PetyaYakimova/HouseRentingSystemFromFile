using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Services.ApplicationUser;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests : UnitTestsBase
    {
        private IUserService _userService;

        [OneTimeSetUp]
        public void SetUp() => _userService = new UserService(_data, _mapper);

        [Test]
        public async Task UserHasRents_ShouldReturnTrue_WithValidData()
        {
            //Arrange

            //Act
            var result = await _userService.UserHasRents(Renter.Id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserFullName_ShouldReturnCorrectResult()
        {
            //Arrange

            //Act
            var result = await _userService.UserFullName(Renter.Id);

            //Assert
            var renterFullName = Renter.FirstName + " " + Renter.LastName;
            Assert.That(result, Is.EqualTo(renterFullName));
        }

        [Test]
        public async Task All_ShouldReturnCorrectUsersAndAgents()
        {
            //Arrange

            //Act
            var result = await _userService.All();

            //Assert
            var usersCount = _data.Users.Count();
            var resultUsers = result.ToList();
            Assert.That(resultUsers.Count(), Is.EqualTo(usersCount));

            var agentsCount = _data.Agents.Count();
            var resultAgents = resultUsers.Where(us => us.PhoneNumber != "");
            Assert.That(resultAgents.Count(), Is.EqualTo(agentsCount));

            var agentUser = resultAgents.FirstOrDefault(ag => ag.Email == Agent.User.Email);
            Assert.IsNotNull(agentUser);
            Assert.That(agentUser.PhoneNumber, Is.EqualTo(Agent.PhoneNumber));
        }
    }
}
