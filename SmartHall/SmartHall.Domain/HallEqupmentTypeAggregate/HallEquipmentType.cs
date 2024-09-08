using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallEqupmentAggregateType
{
    public sealed class HallEquipmentType : AggregateRoot<HallEquipmentTypeId>
    {
        private readonly List<HallId> _halls;

        public HallEquipmentType(HallEquipmentTypeId id, string name, Cost cost, List<HallId> halls) : base(id)
		{
            Name = name;
            Cost = cost;
            _halls = halls;
        } 

        public string Name { get; private set; }

        public Cost Cost { get; private set; }

        public IReadOnlyCollection<HallId> Halls => _halls;
    }
}
