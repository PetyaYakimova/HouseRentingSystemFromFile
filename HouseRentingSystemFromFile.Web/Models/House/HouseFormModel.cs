﻿using HouseRentingSystemFromFile.Core.Contracts;
using HouseRentingSystemFromFile.Core.Models.House;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemFromFile.Data.Data.DataConstants;

namespace HouseRentingSystemFromFile.Web.Models.House
{
	public class HouseFormModel : IHouseModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(0.00, MxPricePerMonth,
            ErrorMessage = "Price Per Month must be a positive number and less than {2} leva.")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
    }
}
