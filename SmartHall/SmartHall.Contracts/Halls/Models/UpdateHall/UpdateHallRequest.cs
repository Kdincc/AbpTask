using SmartHall.Common.Halls.Models.CreateHall;

namespace SmartHall.Common.Halls.Models.UpdateHall
{
    public record UpdateHallRequest(Guid HallId, string Name, decimal BaseCost, int Capacity, List<CreateHallEquipmentDto> HallEquipment);
}
