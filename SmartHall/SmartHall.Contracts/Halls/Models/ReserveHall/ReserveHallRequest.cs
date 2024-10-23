using SmartHall.Common.Halls.Models.Dtos;

namespace SmartHall.Common.Halls.Models.ReserveHall
{
    public record ReserveHallRequest(Guid HallId, DateTime ReservationDateTime, int Hours, List<HallEquipmentDto> SelectedEquipment);
}
