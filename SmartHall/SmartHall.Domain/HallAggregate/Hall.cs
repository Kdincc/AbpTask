using SmartHall.Domain.Common;
using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate
{
    public sealed class Hall : AggregateRoot<HallId>
	{
		private readonly List<Entities.Reservation.Reservation> _reservations;
		private List<HallEquipment> _equipment;

		private Hall(HallId id) : base(id)
		{
			_reservations = [];
			_equipment = [];
		}

		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment, List<Entities.Reservation.Reservation> reservations) : base(id)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			_equipment = hallEquipment;
			_reservations = reservations;
			_equipment = hallEquipment;
		}

		public string Name { get; private set; }

		public Capacity Capacity { get; private set; }

		public Cost BaseCost { get; private set; }

		public IReadOnlyCollection<HallEquipment> HallEquipment => _equipment;

		public IReadOnlyCollection<Entities.Reservation.Reservation> Reservations => _reservations;

		public void Update(string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			_equipment = hallEquipment;
		}

		public Cost Reserve(Entities.Reservation.Reservation reservation, List<HallEquipment> selectedEquipment, IHallReservationStrategy reservationStrategy)
		{
			if (_reservations.Any(r => r.Period.Overlapse(reservation.Period)))
		    {
				throw new ArgumentException("Reservation period overlapse with another reservation");
			}

			_reservations.Add(reservation);

			return reservationStrategy.CalculateCost(this, reservation, selectedEquipment);
		}
	}
}
