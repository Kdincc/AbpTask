namespace SmartHall.Contracts.Halls.CreateHall
{
	public record CreateHallRequest(string HallName, int Capacity, List<CreateHallEquipmentDto> Equipment, decimal BaseHallCost);
}
