using SmartHall.Domain.Common.Models;
using SmartHall.Domain.Common.ValueObjects;

namespace SmartHall.Domain.HallAggregate.Entities.HallEquipment
{
	public sealed class HallEquipment : AggregateRoot<Guid>
	{
		private HallEquipment() : base()
		{
		}

		public HallEquipment(Guid id, string name, Cost cost, Guid hallId) : base(id)
		{
			Name = name;
			Cost = cost;
			HallId = hallId;
		}

		public string Name { get; private set; }

		public Cost Cost { get; private set; }

		public Guid HallId { get; private set; }

		public bool IsSameAs(HallEquipment equipment)
		{
			return Name == equipment.Name && Cost == equipment.Cost;
		}
	}
}
