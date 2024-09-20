using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;

namespace SmartHall.Application.Common.Mapping
{
	public static class HallsMappings
	{
		public static HallDto ToDto(this Hall hall)
		{
			return new HallDto(hall.Id, hall.Name, hall.AvailableEquipment.Select(e => e.ToDto()).ToList());
		}

		public static HallEquipmentDto ToDto(this HallEquipment hallEquipment)
		{
			return new HallEquipmentDto(hallEquipment.Id, hallEquipment.Name, hallEquipment.Cost.Value, hallEquipment.HallId);
		}

		public static HallEquipment FromDto(this HallEquipmentDto hallEquipmentDto)
		{
			return new HallEquipment(hallEquipmentDto.Id, hallEquipmentDto.Name, Cost.Create(hallEquipmentDto.Cost), hallEquipmentDto.HallId);
		}

		public static HallEquipment FromDto(this CreateHallEquipmentDto dto, Guid hallId)
		{
			return new HallEquipment(Guid.NewGuid(), dto.Name, Cost.Create(dto.Cost), hallId);
		}
	}
}
