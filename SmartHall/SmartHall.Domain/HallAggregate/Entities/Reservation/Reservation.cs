using SmartHall.Domain.Common.Models;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities.Reservation
{
	public sealed class Reservation : Entity<Guid>
	{
		private Reservation() : base()
		{
		}

		public Reservation(Guid id, ReservationPeriod period, Guid hallId) : base(id)
		{
			Period = period;
			HallId = hallId;
		}

		public ReservationPeriod Period { get; private set; }

		public Guid HallId { get; private set; }
	}
}
