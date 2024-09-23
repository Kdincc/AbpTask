using SmartHall.Contracts.Halls.Dtos;

namespace SmartHall.Contracts.Halls.ReserveHall
{
	public record ReserveHallRequest(Guid HallId, DateTime ReservationDateTime, int Hours, List<HallEquipmentDto> SelectedEquipment);
}
