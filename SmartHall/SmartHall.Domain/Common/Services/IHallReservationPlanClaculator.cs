using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Services
{
	public interface IHallReservationPlanClaculator
	{
		 public Cost Calculate(Hall hall, Reservation reservation);
	}
}
