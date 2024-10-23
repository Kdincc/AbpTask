using SmartHall.BLL.Halls.HallAggregate;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Halls.Models.Dtos;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Shared.ValueObjects;

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
