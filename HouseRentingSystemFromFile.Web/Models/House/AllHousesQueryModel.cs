using System.ComponentModel.DataAnnotations;
using HouseRentingSystemFromFile.Core;
using HouseRentingSystemFromFile.Core.Models.House;

namespace HouseRentingSystemFromFile.Web.Models.House
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string Category { get; init; } = null!;

        [Display(Name="Search by text")]
        public string SearchTerm { get; init; } = null!;

        public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}
