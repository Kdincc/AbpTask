using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities.HallEquipment
{
    public sealed class HallEquipment : AggregateRoot<ValueObjects.HallEquipmentId>
    {
        private HallEquipment() : base() 
        { 
        }

        public HallEquipment(ValueObjects.HallEquipmentId id, string name, Cost cost, HallId hallId) : base(id)
        {
            Name = name;
            Cost = cost;
            HallId = hallId;
        }

        public string Name { get; private set; }

        public Cost Cost { get; private set; }

        public HallId HallId { get; private set; }

        public bool IsSameAs(HallEquipment equipment)
		{
			return Name == equipment.Name && Cost == equipment.Cost;
		}
    }
}
