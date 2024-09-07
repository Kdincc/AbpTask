using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.HallEqupmentAggregate.ValueObjects
{
	public sealed class HallEquipmentId : ValueObject
	{
        private HallEquipmentId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static HallEquipmentId CreateUnique() => new(Guid.NewGuid());

		public static HallEquipmentId Create(string value) => new(Guid.Parse(value));
	}
}