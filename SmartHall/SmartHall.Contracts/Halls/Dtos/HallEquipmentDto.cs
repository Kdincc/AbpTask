using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.Dtos
{
	public record HallEquipmentDto(Guid Id, string Name, decimal Cost, Guid HallId);
}
