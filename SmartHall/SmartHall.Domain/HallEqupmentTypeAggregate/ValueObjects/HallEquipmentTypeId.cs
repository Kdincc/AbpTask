using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.HallEqupmentAggregate.ValueObjects
{
	public sealed class HallEquipmentTypeId : ValueObject
	{
        private HallEquipmentTypeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static HallEquipmentTypeId CreateUnique() => new(Guid.NewGuid());

		public static HallEquipmentTypeId Create(string value) => new(Guid.Parse(value));
	}
}