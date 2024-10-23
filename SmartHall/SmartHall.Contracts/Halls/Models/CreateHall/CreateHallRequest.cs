namespace SmartHall.Common.Halls.Models.CreateHall
{
    public record CreateHallRequest(string HallName, int Capacity, List<CreateHallEquipmentDto> Equipment, decimal BaseHallCost);
}
