using SmartHall.Domain.Common.Models;
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
		public Hall(HallId id, string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment) : base(id)
		{
			Name = name;
			Capacity = capacity;
			BaseCost = baseCost;
			HallEquipment = hallEquipment;
		}

		public string Name { get; private set; }

		public Capacity Capacity { get; private set; }

		public Cost BaseCost { get; private set; }

		public IReadOnlyCollection<HallEquipment> HallEquipment { get; private set; }

	}
}
