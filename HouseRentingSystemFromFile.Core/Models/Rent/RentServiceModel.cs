namespace HouseRentingSystemFromFile.Core.Models.Rent
{
    public class RentServiceModel
    {
        public string HouseTitle { get; set; } = null!;

        public string HouseImageURL { get; set; } = null!;

        public string AgentFullName { get; set; } = null!;

        public string AgentEmail { get; set; } = null!;

        public string RenterFullName { get; set; } = null!;

        public string RenterEmail { get; set;} = null!;
    }
}
