using SmartHall.Common.BaseModels;

namespace SmartHall.BLL.Halls.HallAggregate.ValueObjects
{
    /// <summary>
    /// Value object for hall capacity
    /// </summary>
    public sealed class Capacity : ValueObject
	{
		public int Value { get; private set; }

		private Capacity(int value)
		{
			Value = value;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static Capacity Create(int value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Capacity cannot be negative");
			}

			return new Capacity(value);
		}
	}
}
