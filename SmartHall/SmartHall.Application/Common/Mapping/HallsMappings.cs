using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
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
			return new HallEquipmentDto(hallEquipment.Id.Value, hallEquipment.Name, hallEquipment.Cost.Value, hallEquipment.HallId.Value);
		}

		public static HallEquipment FromDto(this HallEquipmentDto hallEquipmentDto)
		{
			return new HallEquipment(HallEquipmentId.Create(hallEquipmentDto.Id.ToString()), hallEquipmentDto.Name, Cost.Create(hallEquipmentDto.Cost), HallId.Create(hallEquipmentDto.HallId.ToString()));
		}

		public static HallEquipment FromDto(this CreateHallEquipmentDto dto, HallId hallId)
		{
			return new HallEquipment(HallEquipmentId.CreateUnique(), dto.Name, Cost.Create(dto.Cost), hallId);
		}
	}
}
