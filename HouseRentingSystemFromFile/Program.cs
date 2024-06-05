using HouseRentingSystemFromFile.Contracts.Agent;
using HouseRentingSystemFromFile.Contracts.ApplicationUser;
using HouseRentingSystemFromFile.Contracts.House;
using HouseRentingSystemFromFile.Contracts.Statistic;
using HouseRentingSystemFromFile.Data;
using HouseRentingSystemFromFile.Data.Models;
using HouseRentingSystemFromFile.Services.Agent;
using HouseRentingSystemFromFile.Services.ApplicationUser;
using HouseRentingSystemFromFile.Services.House;
using HouseRentingSystemFromFile.Services.Statistic;
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
	.AddEntityFrameworkStores<HouseRentingDbContext>();

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IStatisticService, StatisticService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();

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
		name: "House Details",
		pattern: "/House/Details/{id}/{information}",
		defaults: new { Controller = "House", Action = "Details" });

	endpoints.MapDefaultControllerRoute();
	endpoints.MapRazorPages();
});

app.Run();
