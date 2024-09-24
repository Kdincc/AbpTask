namespace SmartHall.Contracts.Halls.Dtos
{
	public record HallEquipmentDto(Guid Id, string Name, decimal Cost, Guid HallId);
}
