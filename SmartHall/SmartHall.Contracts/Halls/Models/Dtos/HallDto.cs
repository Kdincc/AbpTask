namespace SmartHall.Common.Halls.Models.Dtos
{
    public record HallDto(Guid Id, string Name, List<HallEquipmentDto> HallEquipment);
}
