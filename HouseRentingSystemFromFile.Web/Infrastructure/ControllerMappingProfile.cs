using AutoMapper;
using HouseRentingSystemFromFile.Core.Models.House;
using HouseRentingSystemFromFile.Web.Models.House;

namespace HouseRentingSystemFromFile.Web.Infrastructure
{
	public class ControllerMappingProfile : Profile
	{
		public ControllerMappingProfile() 
		{
			CreateMap<HouseDetailsServiceModel, HouseFormModel>();
			CreateMap<HouseDetailsServiceModel, HouseDetailsViewModel>();
		}
	}
}
