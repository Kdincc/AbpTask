using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;
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
		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipmentId> hallEquipment) : base(id)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			HallEquipment = hallEquipment;
		}

		public string Name { get; private set; }

		public Capacity Capacity { get; private set; }

		public Cost BaseCost { get; private set; }

		public IReadOnlyCollection<HallEquipmentId> HallEquipment { get; private set; }
	}
}
