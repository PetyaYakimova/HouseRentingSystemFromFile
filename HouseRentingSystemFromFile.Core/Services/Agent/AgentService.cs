using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemFromFile.Core.Services.Agent
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext _data;

        public AgentService(HouseRentingDbContext data)
        {
            _data = data;
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await _data.Agents.AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> AgentWithPhoneNumberExists(string phoneNumber)
        {
            return await _data.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Data.Data.Models.Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await _data.Agents.AddAsync(agent);
            await _data.SaveChangesAsync();
        }

        public async Task<int> GetAgentId(string userId)
        {
            return (await _data.Agents.FirstOrDefaultAsync(a => a.UserId == userId)).Id;
        }
    }
}
