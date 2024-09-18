using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Persistance
{
	public interface IHallRepository : IRepository<Hall>
	{
		public Task<Hall> GetByIdAsync(HallId id, CancellationToken cancellationToken);

		public Task<IEnumerable<Hall>> GetAllWithEquipment(CancellationToken cancellationToken);

		public Task<Hall> GetByIdWithEquipmentAndReservations(HallId id, CancellationToken cancellationToken);
	}
}
