using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Mapping
{
	public static class HallsMappings
	{
		public static HallDto ToDto(this Hall hall)
		{
			return new HallDto(hall.Id.Value, hall.Name, hall.AvailableEquipment.Select(e => e.ToDto()).ToList());
		}

		public static HallEquipmentDto ToDto(this HallEquipment hallEquipment)
		{
			return new HallEquipmentDto(hallEquipment.Id.Value, hallEquipment.Name, hallEquipment.Cost.Value);
		}
	}
}
