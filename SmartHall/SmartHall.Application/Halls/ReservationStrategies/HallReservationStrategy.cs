using SmartHall.BLL.Halls.HallAggregate.Entities.Reservation;
using SmartHall.Common.Halls;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Shared.Constants.Halls;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.BLL.Halls.ReservationStrategies
{
    /// <summary>
    /// Strategy for calculating hall reservation cost
    /// </summary>
    public sealed class HallReservationStrategy : IHallReservationStrategy
    {
        private readonly Dictionary<Func<TimeSpan, bool>, Func<Cost, Cost>> _planHandlers = new()
        {
            { ReservationPlans.PeakPlan.IsWithin, cost => cost * ReservationPlans.PeakPlan.Modifier },
            { ReservationPlans.MorningPlan.IsWithin, cost => cost * ReservationPlans.MorningPlan.Modifier },
            { ReservationPlans.EveningPlan.IsWithin, cost => cost * ReservationPlans.EveningPlan.Modifier },
            { ReservationPlans.Default.IsWithin, cost => cost * ReservationPlans.Default.Modifier }
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
                var planHandler = _planHandlers.First(x => x.Key(current.TimeOfDay)).Value;

                totalCost += planHandler(hourlyCost);

                current = current.AddHours(1);
            }

            return totalCost;
        }
    }
}
