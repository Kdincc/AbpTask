using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregate;
using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
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
		private List<HallEquipmentTypeId> _equipment;

		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipmentTypeId> hallEquipment, List<Reservation> reservations) : base(id)
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

		public IEnumerable<HallEquipmentTypeId> HallEquipment => _equipment;

		public IEnumerable<Reservation> Reservations => _reservations;

		public void Update(string name, Capacity capacity, Cost baseCost, List<HallEquipmentTypeId> hallEquipment)
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
