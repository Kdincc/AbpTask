using SmartHall.Contracts.Halls.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.CreateHall
{
	public record CreateHallRequest(string HallName, int Capacity, List<CreateHallEquipmentDto> Equipment, decimal BaseHallCost);
}
