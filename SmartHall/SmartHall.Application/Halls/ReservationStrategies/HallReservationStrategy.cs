using SmartHall.Domain.Common;
using SmartHall.Domain.Common.Constanst.Halls;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.ReservationStrategies
{
	public sealed class HallReservationStrategy : IHallReservationStrategy
	{
		private readonly Dictionary<Func<TimeSpan, bool>, Func<Cost, Cost>> plansHandlings = new()
		{
			{ ReservationPlans.PeakPlan.IsWithin, cost => cost * ReservationPlans.PeakPlan.Modyfier },
			{ ReservationPlans.MorningPlan.IsWithin, cost => cost * ReservationPlans.MorningPlan.Modyfier },
			{ ReservationPlans.EveningPlan.IsWithin, cost => cost * ReservationPlans.EveningPlan.Modyfier },
			{ ReservationPlans.Default.IsWithin, cost => cost * ReservationPlans.Default.Modyfier }
		};

		public Cost CalculateCost(Hall hall, Reservation reservation, List<HallEquipment> selected)
		{
			Cost selectedEquipmentCost = Cost.Create(0);

			if (selected.Count > 0)
			{
				selectedEquipmentCost = selected.Aggregate(Cost.Create(0), (acc, equipment) => acc + equipment.Cost);
			}
			 
			Cost hourlyCost = hall.BaseCost + selectedEquipmentCost;
			Cost totalCost = Cost.Create(0);
			DateTime current = reservation.Period.Start;
			DateTime end = reservation.Period.End;

			while (current < end)
			{
				var planHandler = plansHandlings.First(x => x.Key(current.TimeOfDay)).Value;

				totalCost += planHandler(hourlyCost);

				current = current.AddHours(1);
			}

			return totalCost;
		}
	}
}
