using SmartHall.Domain.Common.Models;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities
{
    public sealed class HallEquipment : Entity<HallEquipmentId>
    {
        private HallEquipment(HallEquipmentId id, string name, Cost cost) : base(id)
		{
			Name = name;
			Cost = cost;
		}

        public string Name { get; private set; }

        public Cost Cost { get; private set; }

        public static HallEquipment Create(HallEquipmentId id, string name, Cost cost)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            return new HallEquipment(id, name, cost);
        }

    }
}
