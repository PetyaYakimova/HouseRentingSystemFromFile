using AutoMapper;
using HouseRentingSystemFromFile.Core.Infrastructure;

namespace HouseRentingSystemFromFile.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<ServiceMappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
