using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregateType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Persistance
{
	public interface IHallEquipmentTypeRepository : IRepository<HallEquipmentType>
	{
		public Task<HallEquipmentType> GetByIdAsync(HallEquipmentTypeId id, CancellationToken cancellationToken);
	}
}
