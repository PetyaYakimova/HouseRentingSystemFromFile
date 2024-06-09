using System.Security.Claims;
using static HouseRentingSystemFromFile.Data.Data.AdminUser;

namespace HouseRentingSystemFromFile.Web.Infrastructure
{
    public static class ClaimsPrincipleExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
