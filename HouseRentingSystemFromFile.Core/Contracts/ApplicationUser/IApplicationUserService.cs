using HouseRentingSystemFromFile.Core.Models.User;

namespace HouseRentingSystemFromFile.Core.Contracts.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<string?> UserFullName(string userId);

        Task<IEnumerable<UserServiceModel>> All();
    }
}
