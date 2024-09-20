using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;

namespace SmartHall.Domain.Common
{
	public interface IHallReservationStrategy
	{
		public Cost CalculateCost(Hall hall, HallAggregate.Entities.Reservation.Reservation reservation, List<HallEquipment> selected);
	}
}
