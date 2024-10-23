using SmartHall.Common.BaseModels;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.Reservation.ValueObjects;

namespace SmartHall.BLL.Halls.HallAggregate.Entities.Reservation
{
    /// <summary>
    /// Entity for hall reservation, part of hall aggregate
    /// </summary>
    public sealed class Reservation : Entity<Guid>
	{
		private Reservation() : base()
		{
		}

		public Reservation(Guid id, ReservationPeriod period, Guid hallId) : base(id)
		{
			Period = period;
			HallId = hallId;
		}

		public ReservationPeriod Period { get; private set; }

		public Guid HallId { get; private set; }
	}
}
