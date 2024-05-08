using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemFromFile.Data.DataConstants;

namespace HouseRentingSystemFromFile.Models.Agent
{
	public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
