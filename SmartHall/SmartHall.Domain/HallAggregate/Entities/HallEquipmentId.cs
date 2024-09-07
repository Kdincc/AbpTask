using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.HallAggregate.Entities
{
	public class HallEquipmentId : ValueObject
	{
		public Guid Value { get; private set; }

		private HallEquipmentId(Guid value)
		{
			Value = value;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static HallEquipmentId CreateUnique() => new(Guid.NewGuid());

		public static HallEquipmentId Create(string value) => new(Guid.Parse(value));
	}
}