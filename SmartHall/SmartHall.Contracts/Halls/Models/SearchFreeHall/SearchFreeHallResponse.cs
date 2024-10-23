using SmartHall.Common.Halls.Models.Dtos;

namespace SmartHall.Common.Halls.Models.SearchFreeHall
{
    public record SearchFreeHallResponse(List<HallDto> AvailableHalls);
}
