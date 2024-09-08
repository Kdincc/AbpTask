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
		public Reservation(ReservationId id, ReservationPeriod period) : base(id)
		{
			Period = period;
		}

		public ReservationPeriod Period { get; private set; }

		public HallId HallId { get; private set; }
	}
}
