using SmartHall.Domain.Common.Models;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities.Reservation
{
	public sealed class Reservation : Entity<ReservationId>
	{
		public Reservation(ReservationId id) : base(id)
		{
		}
	}
}
