using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static HouseRentingSystemFromFile.Data.DataConstants;

namespace HouseRentingSystemFromFile.Data.Models
{
	public class Agent
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(PhoneNumberMaxLength)]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		public string UserId { get; set; } = null!;

		[ForeignKey(nameof(UserId))]
		public ApplicationUser User { get; set; } = null!;
	}
}
