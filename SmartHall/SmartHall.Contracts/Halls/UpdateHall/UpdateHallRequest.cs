using SmartHall.Contracts.Halls.CreateHall;

namespace SmartHall.Contracts.Halls.UpdateHall
{
	public record UpdateHallRequest(Guid HallId, string Name, decimal BaseCost, int Capacity, List<CreateHallEquipmentDto> HallEquipment);
}
