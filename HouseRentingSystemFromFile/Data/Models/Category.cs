﻿using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemFromFile.Data.DataConstants;

namespace HouseRentingSystemFromFile.Data.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public IEnumerable<House> Houses { get; init; } = new List<House>();
	}
}
