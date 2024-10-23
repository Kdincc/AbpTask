namespace SmartHall.Common.Halls.Models.Dtos
{
    public record HallEquipmentDto(Guid Id, string Name, decimal Cost, Guid HallId);
}
