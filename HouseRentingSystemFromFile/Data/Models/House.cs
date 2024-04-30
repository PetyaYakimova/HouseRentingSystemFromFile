using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystemFromFile.Data.DataConstants;

namespace HouseRentingSystemFromFile.Data.Models
{
	public class House
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(AddressMaxLength)]
		public string Address { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public decimal PricePerMonth { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		[Required]
		public int AgentId { get; set; }

		[ForeignKey(nameof(AgentId))]
		public Agent Agent { get; set; } = null!;

		public string? RenterId { get; set; }
	}
}
