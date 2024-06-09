namespace HouseRentingSystemFromFile.Core.Contracts.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<string?> UserFullName(string userId);
    }
}
