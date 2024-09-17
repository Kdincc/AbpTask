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
	public sealed class Reservation : Entity<ReservationId>
	{
		private Reservation() : base()
		{
		}

		public Reservation(ReservationId id, ReservationPeriod period, HallId hallId) : base(id)
		{
			Period = period;
			HallId = hallId;
		}

		public ReservationPeriod Period { get; private set; }

		public HallId HallId { get; private set; }
	}
}
