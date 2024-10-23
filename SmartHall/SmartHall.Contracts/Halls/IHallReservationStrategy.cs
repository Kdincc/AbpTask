using SmartHall.BLL.Halls.HallAggregate.Entities.Reservation;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.Common.Halls
{
    /// <summary>
    /// Interface for hall reservation strategy, calculates cost of reservation for given hall with selected equipment
    /// </summary>
    public interface IHallReservationStrategy
    {
        public Cost CalculateCost(Hall hall, Reservation reservation, List<HallEquipment> selected);
    }
}
