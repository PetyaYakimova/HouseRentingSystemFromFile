using AutoMapper;
using AutoMapper.QueryableExtensions;
using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Models.User;
using HouseRentingSystemFromFile.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemFromFile.Core.Services.ApplicationUser
{
	public class ApplicationUserService : IApplicationUserService
	{
		private readonly HouseRentingDbContext _data;
		private readonly IMapper _mapper;

		public ApplicationUserService(HouseRentingDbContext data, IMapper mapper)
		{
			_data = data;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserServiceModel>> All()
		{
			var allUsers = new List<UserServiceModel>();

			var agents = await _data
				.Agents
				.Include(ag => ag.User)
				.ProjectTo<UserServiceModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			allUsers.AddRange(agents);

			var users = await _data
				.Users
				.Where(u => !_data.Agents.Any(ag => ag.UserId == u.Id))
				.ProjectTo<UserServiceModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			allUsers.AddRange(users);

			return allUsers;
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
