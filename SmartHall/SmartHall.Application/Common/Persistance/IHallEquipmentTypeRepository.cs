using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Persistance
{
    public interface IHallEquipmentTypeRepository : IRepository<Domain.HallAggregate.Entities.HallEquipment.HallEquipment>
	{
		public Task<Domain.HallAggregate.Entities.HallEquipment.HallEquipment> GetByIdAsync(Domain.HallAggregate.Entities.HallEquipment.ValueObjects.HallEquipmentId id, CancellationToken cancellationToken);
	}
}
