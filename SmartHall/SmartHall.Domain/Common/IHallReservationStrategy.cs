using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common
{
	public interface IHallReservationStrategy
	{
		public Cost CalculateCost(Hall hall, HallAggregate.Entities.Reservation.Reservation reservation, List<HallEquipment> selected);
	}
}
