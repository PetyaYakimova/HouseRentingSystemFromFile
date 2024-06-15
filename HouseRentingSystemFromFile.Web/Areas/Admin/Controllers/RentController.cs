using HouseRentingSystemFromFile.Core.Contracts.Rent;
using HouseRentingSystemFromFile.Core.Models.Rent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Web.Areas.Admin.Controllers
{
    public class RentController : AdminController
    {
        private readonly IRentService _rents;
        private readonly IMemoryCache _cache;

        public RentController(IRentService rents, IMemoryCache cache)
        {
            _rents = rents;
            _cache = cache;
        }

        [Route("Rent/All")]
        public async Task<IActionResult> All()
        {
            var rents = _cache.Get<IEnumerable<RentServiceModel>>(RentsCacheKey);

            if (rents == null)
            {
                rents = await _rents.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(RentsCacheKey, rents, cacheOptions);
            }

            return View(rents);
        }
    }
}
