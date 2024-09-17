using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.UpdateHall
{
	public record UpdateHallRequest(Guid HallId, string Name, decimal BaseCost, int Capacity, List<CreateHallEquipmentDto> HallEquipment);
}
