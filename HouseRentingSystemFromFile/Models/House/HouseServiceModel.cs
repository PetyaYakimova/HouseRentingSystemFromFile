using HouseRentingSystemFromFile.Contracts;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystemFromFile.Models.House
{
    public class HouseServiceModel : IHouseModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Display(Name = "image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
