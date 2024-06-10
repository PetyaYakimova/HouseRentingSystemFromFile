using HouseRentingSystemFromFile.Core.Contracts.Agent;
using HouseRentingSystemFromFile.Core.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Core.Contracts.House;
using HouseRentingSystemFromFile.Core.Contracts.Statistic;
using HouseRentingSystemFromFile.Core.Services.Agent;
using HouseRentingSystemFromFile.Core.Services.ApplicationUser;
using HouseRentingSystemFromFile.Core.Services.House;
using HouseRentingSystemFromFile.Core.Services.Statistic;
using HouseRentingSystemFromFile.Data.Data;
using HouseRentingSystemFromFile.Data.Data.Models;
using HouseRentingSystemFromFile.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HouseRentingDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireNonAlphanumeric = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<HouseRentingDbContext>();

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IStatisticService, StatisticService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();

builder.Services.AddAutoMapper(
	typeof(IHouseService).Assembly,
	typeof(HomeController).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error/500");
	app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "Areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
	endpoints.MapControllerRoute(
		name: "House Details",
		pattern: "/House/Details/{id}/{information}",
		defaults: new { Controller = "House", Action = "Details" });

	endpoints.MapDefaultControllerRoute();
	endpoints.MapRazorPages();
});

app.SeedAdmin();

app.Run();
