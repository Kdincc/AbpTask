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
		private readonly List<Reservation> _reservations;
		private List<HallEquipment> _equipment;

		private Hall(HallId id) : base(id)
		{
			_reservations = [];
			_equipment = [];
		}

		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment, List<Reservation> reservations) : base(id)
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

		public IReadOnlyCollection<Reservation> Reservations => _reservations;

		public void Update(string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			_equipment = hallEquipment;
		}

		public void Reserve(Reservation reservation)
		{
			if (_reservations.Any(r => r.Period.Overlapse(reservation.Period)))
		    {
				throw new ArgumentException("Reservation period overlapse with another reservation");
			}

			_reservations.Add(reservation);
		}
	}
}
