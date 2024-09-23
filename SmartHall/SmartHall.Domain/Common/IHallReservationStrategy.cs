using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.Reservation;

namespace SmartHall.Domain.Common
{
	/// <summary>
	/// Interface for hall reservation strategy, calculates cost of reservation for given hall with selected equipment
	/// </summary>
	public interface IHallReservationStrategy
	{
		public Cost CalculateCost(Hall hall, Reservation reservation, List<HallEquipment> selected);
	}
}
