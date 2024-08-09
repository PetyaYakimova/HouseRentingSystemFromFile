using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Core.Services.Agent;

namespace HouseRentingSystemFromFile.Tests.UnitTests
{
    [TestFixture]
    public class AgentServiceTests : UnitTestsBase
    {
        private IAgentService _agentService;

        [OneTimeSetUp]
        public void SetUp() => _agentService = new AgentService(_data);

        [Test]
        public async Task GetAgentId_ShouldReturnCorrectUserId()
        {
            //Arrange

            //Act
            var resultAgentId = await _agentService.GetAgentId(Agent.UserId);

            //Assert
            Assert.That(Convert.ToInt32(resultAgentId), Is.EqualTo(Agent.Id));
        }

        [Test]
        public async Task ExistsById_SHouldReturnTrue_WithValidIdAsync()
        {
            //Arrange

            //Act
            var result = await _agentService.ExistsById(Agent.UserId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AgentWithPhoneNumberExists_ShouldReturnTrue_WithValidData()
        {
            //Arrange

            //Act
            var result = await _agentService.AgentWithPhoneNumberExists(Agent.PhoneNumber);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateAgent_ShouldWorkCorrectly()
        {
            //Arrange
            var agentsCountBefore = _data.Agents.Count();

            //Act
            await _agentService.Create(Agent.UserId, Agent.PhoneNumber);

            //Assert
            var agentsCountAfter = _data.Agents.Count();
            Assert.That(agentsCountAfter, Is.EqualTo(agentsCountBefore + 1));

            var newAgentId = await _agentService.GetAgentId(Agent.UserId);
            var newAgentInDb = await _data.Agents.FindAsync(newAgentId);
            Assert.IsNotNull(newAgentInDb);
            Assert.That(newAgentInDb.UserId, Is.EqualTo(Agent.UserId));
            Assert.That(newAgentInDb.PhoneNumber, Is.EqualTo(Agent.PhoneNumber));
        }
    }
}
