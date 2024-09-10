using SmartHall.Contracts.Halls.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.SearchFreeHall
{
	public record SearchFreeHallResponse(List<HallDto> AvailableHalls);
}
