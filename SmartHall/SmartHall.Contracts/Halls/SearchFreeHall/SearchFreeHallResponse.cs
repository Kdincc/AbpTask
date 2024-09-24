using SmartHall.Contracts.Halls.Dtos;

namespace SmartHall.Contracts.Halls.SearchFreeHall
{
	public record SearchFreeHallResponse(List<HallDto> AvailableHalls);
}
