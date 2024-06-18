using HouseRentingSystemFromFile.Data.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemFromFile.Data.Data.DataConstants;
using static HouseRentingSystemFromFile.Data.Data.AdminConstants;

namespace HouseRentingSystemFromFile.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMemoryCache _cache;

		public RegisterModel(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IMemoryCache cache)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_cache = cache;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public class InputModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; } = null!;

			[Required]
			[Display(Name = "First Name")]
			[StringLength(UserFirstNameMaxLength,
				MinimumLength = UserFirstNameMinLength)]
			public string FirstName { get; set; } = null!;

			[Required]
			[Display(Name = "Last Name")]
			[StringLength(UserLastNameMaxLength,
			   MinimumLength = UserLastNameMinLength)]
			public string LastName { get; set; } = null!;

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; } = null!;

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; } = null!;
		}


		public async Task OnGetAsync(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");

			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = Input.Email,
					Email = Input.Email,
					FirstName = Input.FirstName,
					LastName = Input.LastName
				};

				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					_cache.Remove(UsersCacheKey);
					return LocalRedirect(returnUrl);
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return Page();
		}
	}
}
