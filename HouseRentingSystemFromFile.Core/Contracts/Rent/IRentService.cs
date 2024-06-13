using HouseRentingSystemFromFile.Core.Models.Rent;

namespace HouseRentingSystemFromFile.Core.Contracts.Rent
{
    public interface IRentService
    {

        Task<IEnumerable<RentServiceModel>> All();
    }
}
