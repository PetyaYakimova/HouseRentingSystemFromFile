using HouseRentingSystemFromFile.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Data.Data
{
    public class HouseRentingDbContext : IdentityDbContext<ApplicationUser>
    {
        private bool _seedDb;

        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options, bool seedDb)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
            else
            {
                Database.EnsureCreated();
            }

            _seedDb = seedDb;
        }

        public DbSet<House> Houses { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Agent> Agents { get; set; } = null!;

        private ApplicationUser AgentUser { get; set; } = null!;

        private ApplicationUser GuestUser { get; set; } = null!;

        private ApplicationUser AdminUser { get; set; } = null!;

        private Agent AdminAgent { get; set; } = null!;

        private Agent Agent { get; set; } = null!;

        private Category CottageCategory { get; set; } = null!;

        private Category SingleCategory { get; set; } = null!;

        private Category DuplexCategory { get; set; } = null!;

        private House FirstHouse { get; set; } = null!;

        private House SecondHouse { get; set; } = null!;

        private House ThirdHouse { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<House>()
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<House>()
                .HasOne(h => h.Agent)
                .WithMany()
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            if (_seedDb)
            {
                SeedUsers();
                builder.Entity<ApplicationUser>()
                    .HasData(AgentUser, GuestUser, AdminUser);

                SeedAgent();
                builder.Entity<Agent>()
                    .HasData(Agent, AdminAgent);

                SeedCategories();
                builder.Entity<Category>()
                    .HasData(CottageCategory, SingleCategory, DuplexCategory);

                SeedHouses();
                builder.Entity<House>()
                    .HasData(FirstHouse, SecondHouse, ThirdHouse);
            }

            base.OnModelCreating(builder);
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AgentUser = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com",
                FirstName = "Linda",
                LastName = "Michaels"
            };

            AgentUser.PasswordHash =
                 hasher.HashPassword(AgentUser, "agent123");

            GuestUser = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                FirstName = "Teodor",
                LastName = "Lesly"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(GuestUser, "guest123");

            AdminUser = new ApplicationUser()
            {
                Id = "8be94639-2c1d-437f-8df0-3eb5f4075427",
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail,
                Email = AdminEmail,
                NormalizedEmail = AdminEmail,
                FirstName = "Great",
                LastName = "Admin"
            };

            AdminUser.PasswordHash =
            hasher.HashPassword(AdminUser, "admin123");
        }

        private void SeedAgent()
        {
            Agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = AgentUser.Id
            };

            AdminAgent = new Agent()
            {
                Id = 5,
                PhoneNumber = "+359123456789",
                UserId = AdminUser.Id
            };
        }

        private void SeedCategories()
        {
            CottageCategory = new Category()
            {
                Id = 1,
                Name = "Cottage"
            };

            SingleCategory = new Category()
            {
                Id = 2,
                Name = "Single-Family"
            };

            DuplexCategory = new Category()
            {
                Id = 3,
                Name = "Duplex"
            };
        }

        private void SeedHouses()
        {
            FirstHouse = new House()
            {
                Id = 1,
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = DuplexCategory.Id,
                AgentId = Agent.Id,
                RenterId = GuestUser.Id
            };

            SecondHouse = new House()
            {
                Id = 2,
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = SingleCategory.Id,
                AgentId = Agent.Id
            };

            ThirdHouse = new House()
            {
                Id = 3,
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = SingleCategory.Id,
                AgentId = Agent.Id
            };
        }

    }
}