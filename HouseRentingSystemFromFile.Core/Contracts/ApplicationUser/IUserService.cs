using HouseRentingSystemFromFile.Core.Models.User;

namespace HouseRentingSystemFromFile.Core.Contracts.ApplicationUser
{
    public interface IUserService
    {
        Task<string?> UserFullName(string userId);

        Task<IEnumerable<UserServiceModel>> All();

        Task<bool> UserHasRents(string userId);
    }
}
