using SmartHall.Contracts.Halls.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.ReserveHall
{
	public record ReserveHallRequest(Guid HallId, DateTime ReservationDateTime, TimeSpan Duratation, List<HallEquipmentDto> SelectedEquipment);
}
