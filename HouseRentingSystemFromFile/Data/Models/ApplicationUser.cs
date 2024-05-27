﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemFromFile.Data.DataConstants;

namespace HouseRentingSystemFromFile.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[MaxLength(UserFirstNameMaxLength)]
		public string FirstName { get; set; } = null!;

		[Required]
		[MaxLength(UserLastNameMaxLength)]
		public string LastName { get; set; } = null!;
	}
}
