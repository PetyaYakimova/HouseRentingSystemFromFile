using HouseRentingSystemFromFile.Models.Agent;

namespace HouseRentingSystemFromFile.Models.House
{
	public class HouseDetailsServiceModel : HouseServiceModel
	{

		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;

		public AgentServiceModel Agent { get; set; } = null!;
	}
}
