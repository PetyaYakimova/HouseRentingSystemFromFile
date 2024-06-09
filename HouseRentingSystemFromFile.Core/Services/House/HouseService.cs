using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Contracts.House;
using HouseRentingSystemFromFile.Data.Data;
using HouseRentingSystemFromFile.Core.Models.Agent;
using HouseRentingSystemFromFile.Core.Models.House;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace HouseRentingSystemFromFile.Core.Services.House
{
	public class HouseService : IHouseService
	{
		private readonly HouseRentingDbContext _data;
		private readonly IApplicationUserService _user;
		private readonly IMapper _mapper;

		public HouseService(HouseRentingDbContext data,
			IApplicationUserService user,
			IMapper mapper)
		{
			_data = data;
			_user = user;
			_mapper = mapper;
		}

		public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
		{
			return await _data
				.Houses
				.OrderByDescending(c => c.Id)
				.ProjectTo<HouseIndexServiceModel>(_mapper.ConfigurationProvider)
				.Take(3)
				.ToListAsync();
		}

		public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategories()
		{
			return await _data.Categories
				.ProjectTo<HouseCategoryServiceModel>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<bool> CategoryExists(int categoryId)
		{
			return await _data.Categories.AnyAsync(c => c.Id == categoryId);
		}

		public async Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId,
			int agentId)
		{
			var house = new Data.Data.Models.House()
			{
				Title = title,
				Address = address,
				Description = description,
				ImageUrl = imageUrl,
				PricePerMonth = price,
				CategoryId = categoryId,
				AgentId = agentId
			};

			await _data.Houses.AddAsync(house);
			await _data.SaveChangesAsync();

			return house.Id;
		}

		public HouseQueryServiceModel All(string? category = null, string? searchTerm = null,
			HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
		{
			var housesQuery = _data.Houses.AsQueryable();

			if (!string.IsNullOrWhiteSpace(category))
			{
				housesQuery = _data.Houses
					.Where(h => h.Category.Name == category);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				housesQuery = housesQuery
					.Where(h =>
						h.Title.ToLower().Contains(searchTerm.ToLower()) ||
						h.Address.ToLower().Contains(searchTerm.ToLower()) ||
						h.Description.ToLower().Contains(searchTerm.ToLower()));
			}

			housesQuery = sorting switch
			{
				HouseSorting.Price => housesQuery
					.OrderBy(h => h.PricePerMonth),
				HouseSorting.NotRentedFirst => housesQuery
					.OrderBy(h => h.RenterId != null)
					.ThenByDescending(h => h.Id),
				_ => housesQuery.OrderByDescending(h => h.Id)
			};

			var houses = housesQuery
				.Skip((currentPage - 1) * housesPerPage)
				.Take(housesPerPage)
				.ProjectTo<HouseServiceModel>(_mapper.ConfigurationProvider)
				.ToList();

			var totalHouses = housesQuery.Count();

			return new HouseQueryServiceModel()
			{
				TotalHousesCount = totalHouses,
				Houses = houses
			};
		}

		public async Task<IEnumerable<string>> AllCategoriesNames()
		{
			return await _data.Categories
				.Select(c => c.Name)
				.Distinct()
				.ToListAsync();
		}

		public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId)
		{
			var houses = await _data
				.Houses
				.Where(h => h.AgentId == agentId)
				.ProjectTo<HouseServiceModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return houses;
		}

		public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
		{
			var houses = await _data
				.Houses
				.Where(h => h.RenterId == userId)
				.ProjectTo<HouseServiceModel>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return houses;
		}

		public async Task<bool> Exists(int id)
		{
			return await _data
				.Houses
				.AnyAsync(h => h.Id == id);
		}

		public async Task<HouseDetailsServiceModel?> HouseDetailsById(int id)
		{
			var dbHouse = await _data
				.Houses
				.Include(h => h.Category)
				.Include(h => h.Agent.User)
				.Where(h => h.Id == id)
				.FirstOrDefaultAsync();

			var house = _mapper.Map<HouseDetailsServiceModel>(dbHouse);

			var agent = _mapper.Map<AgentServiceModel>(dbHouse.Agent);
			agent.FullName = await _user.UserFullName(dbHouse.Agent.UserId);

			house.Agent = agent;

			return house;
		}

		public async Task Edit(int houseId, string title, string address, string description, string imageUrl, decimal price,
			int categoryId)
		{
			var house = await _data.Houses.FindAsync(houseId);

			if (house != null)
			{
				house.Title = title;
				house.Address = address;
				house.Description = description;
				house.ImageUrl = imageUrl;
				house.PricePerMonth = price;
				house.CategoryId = categoryId;

				await _data.SaveChangesAsync();
			}
		}

		public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
		{
			var house = await _data.Houses.FindAsync(houseId);
			if (house == null)
			{
				return false;
			}

			var agent = await _data.Agents.FirstOrDefaultAsync(a => a.Id == house.AgentId);

			if (agent == null)
			{
				return false;
			}

			if (agent.UserId != currentUserId)
			{
				return false;
			}

			return true;
		}

		public async Task<int> GetHouseCategoryId(int houseId)
		{
			return (await _data.Houses.FindAsync(houseId)).CategoryId;
		}

		public async Task Delete(int houseId)
		{
			var house = await _data.Houses.FindAsync(houseId);

			if (house != null)
			{
				_data.Remove(house);

				await _data.SaveChangesAsync();
			}
		}

		public async Task<bool> IsRented(int id)
		{
			var house = await _data.Houses.FindAsync(id);
			var result = house.RenterId != null;
			return result;
		}

		public async Task<bool> IsRentedByUserWithId(int houseId, string userId)
		{
			var house = await _data.Houses.FindAsync(houseId);

			if (house == null)
			{
				return false;
			}

			return house.RenterId == userId;
		}

		public async Task Rent(int houseId, string userId)
		{
			var house = await _data.Houses.FindAsync(houseId);

			house.RenterId = userId;
			await _data.SaveChangesAsync();
		}

		public async Task Leave(int houseId)
		{
			var house = await _data.Houses.FindAsync(houseId);

			house.RenterId = null;
			await _data.SaveChangesAsync();
		}
	}
}
