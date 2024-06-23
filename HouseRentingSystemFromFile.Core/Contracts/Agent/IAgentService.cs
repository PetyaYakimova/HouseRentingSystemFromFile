namespace HouseRentingSystemFromFile.Core.Contracts.Agent
{
    public interface IAgentService
    {
        Task<bool> ExistsById(string userId);

        Task<bool> AgentWithPhoneNumberExists(string phoneNumber);

        Task Create(string userId, string phoneNumber);

        Task<int> GetAgentId(string userId);
    }
}
