using AutoMapper;
using HouseRentingSystemFromFile.Core.Models.Agent;
using HouseRentingSystemFromFile.Core.Models.House;
using HouseRentingSystemFromFile.Data.Data.Models;

namespace HouseRentingSystemFromFile.Core.Infrastructure
{
	public class ServiceMappingProfile : Profile
	{
		public ServiceMappingProfile()
		{
			CreateMap<House, HouseServiceModel>()
				.ForMember(h => h.IsRented, cfg => cfg.MapFrom(h => h.RenterId != null));

			CreateMap<House, HouseDetailsServiceModel>()
				.ForMember(h => h.IsRented, cfg => cfg.MapFrom(h => h.RenterId != null))
				.ForMember(h => h.Category, cfg => cfg.MapFrom(h => h.Category.Name));

			CreateMap<House, HouseIndexServiceModel>();

			CreateMap<Agent, AgentServiceModel>()
				.ForMember(a => a.Email, cfg => cfg.MapFrom(a => a.User.Email));

			CreateMap<Category, HouseCategoryServiceModel>();
		}
	}
}
