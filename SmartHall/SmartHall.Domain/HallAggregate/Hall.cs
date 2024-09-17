using SmartHall.Domain.Common;
using SmartHall.Domain.Common.Comparers;
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
		private List<HallEquipment> _availableEquipment ;

		private Hall() : base()
		{
		}

		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipment> availableEquipment, List<Entities.Reservation.Reservation> reservations) : base(id)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			_availableEquipment = availableEquipment;
			_reservations = reservations;
		}

		public string Name { get; private set; }

		public Capacity Capacity { get; private set; }

		public Cost BaseCost { get; private set; }

		public List<HallEquipment> AvailableEquipment => _availableEquipment;

		public IReadOnlyCollection<Entities.Reservation.Reservation> Reservations => _reservations.AsReadOnly();

		public void Update(string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			_availableEquipment = hallEquipment;
		}

		public Cost Reserve(Entities.Reservation.Reservation reservation, List<HallEquipment> selectedEquipment, IHallReservationStrategy reservationStrategy)
		{
			if (_reservations.Any(r => r.Period.Overlapse(reservation.Period)))
		    {
				throw new ArgumentException("Reservation period overlaps with another reservation");
			}

			_reservations.Add(reservation);

			return reservationStrategy.CalculateCost(this, reservation, selectedEquipment);
		}

		public bool IsSameAs(Hall hall)
		{
			return Name == hall.Name && Capacity == hall.Capacity && BaseCost == hall.BaseCost && HasSameEquipment(hall._availableEquipment);
		}

		public bool HasSameEquipment(List<HallEquipment> equipment)
		{
			var comparer = new HallEquipmentComparer();

			bool areEqual = new HashSet<HallEquipment>(_availableEquipment, comparer).SetEquals(new HashSet<HallEquipment>(equipment, comparer));

			return areEqual;
		}
	}
}
