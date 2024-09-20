namespace SmartHall.Contracts.Halls.Dtos
{
	public record HallDto(Guid Id, string Name, List<HallEquipmentDto> HallEquipment);
}
