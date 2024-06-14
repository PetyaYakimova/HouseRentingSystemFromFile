using AutoMapper;
using HouseRentingSystemFromFile.Core.Models.Agent;
using HouseRentingSystemFromFile.Core.Models.House;
using HouseRentingSystemFromFile.Core.Models.Rent;
using HouseRentingSystemFromFile.Core.Models.User;
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

			CreateMap<House, RentServiceModel>()
				.ForMember(h => h.HouseTitle, cfg => cfg.MapFrom(h => h.Title))
				.ForMember(h => h.HouseImageURL, cfg => cfg.MapFrom(h => h.ImageUrl))
				.ForMember(h => h.AgentFullName, cfg => cfg.MapFrom(h => h.Agent.User.FirstName + " " + h.Agent.User.LastName))
				.ForMember(h => h.AgentEmail, cfg => cfg.MapFrom(h => h.Agent.User.Email))
				.ForMember(h => h.RenterFullName, cfg => cfg.MapFrom(h => h.Renter.FirstName + " " + h.Renter.LastName))
				.ForMember(h => h.RenterEmail, cfg => cfg.MapFrom(h => h.Renter.Email));

			CreateMap<Agent, AgentServiceModel>()
				.ForMember(a => a.Email, cfg => cfg.MapFrom(a => a.User.Email));

			CreateMap<Category, HouseCategoryServiceModel>();

			CreateMap<Agent, UserServiceModel>()
				.ForMember(us => us.Email, cfg => cfg.MapFrom(ag => ag.User.Email))
				.ForMember(us => us.FullName, cfg => cfg.MapFrom(ag => ag.User.FirstName + " " + ag.User.LastName));

			CreateMap<ApplicationUser, UserServiceModel>()
				.ForMember(us => us.PhoneNumber, cfg => cfg.MapFrom(us => string.Empty))
				.ForMember(us => us.FullName, cfg => cfg.MapFrom(us => us.FirstName + " " + us.LastName));
		}
	}
}
