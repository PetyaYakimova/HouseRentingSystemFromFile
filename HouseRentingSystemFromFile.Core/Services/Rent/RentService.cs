using AutoMapper;
using AutoMapper.QueryableExtensions;
using HouseRentingSystemFromFile.Core.Contracts.Rent;
using HouseRentingSystemFromFile.Core.Models.Rent;
using HouseRentingSystemFromFile.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemFromFile.Core.Services.Rent
{
    public class RentService : IRentService
    {
        private readonly HouseRentingDbContext _data;
        private readonly IMapper _mapper;

        public RentService(HouseRentingDbContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentServiceModel>> All()
        {
            return await _data
                .Houses
                .Include(h => h.Agent.User)
                .Include(h => h.Renter)
                .Where(h => h.RenterId != null)
                .ProjectTo<RentServiceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
