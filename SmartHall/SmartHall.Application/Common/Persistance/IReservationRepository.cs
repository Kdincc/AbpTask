using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Persistance
{
	public interface IReservationRepository : IRepository<Reservation>
	{
		public Task<Reservation> GetByIdAsync(ReservationId id, CancellationToken cancellationToken);
	}
}
