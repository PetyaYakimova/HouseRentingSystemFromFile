using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Data.Data;

namespace HouseRentingSystemFromFile.Core.Services.ApplicationUser
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly HouseRentingDbContext _data;

        public ApplicationUserService(HouseRentingDbContext data)
        {
            _data = data;
        }

        public async Task<string?> UserFullName(string userId)
        {
            var user = await _data.Users.FindAsync(userId);

            if (string.IsNullOrEmpty(user.FirstName)
                || string.IsNullOrEmpty(user.LastName))
            {
                return null;
            }

            return user.FirstName + " " + user.LastName;
        }
    }
}
