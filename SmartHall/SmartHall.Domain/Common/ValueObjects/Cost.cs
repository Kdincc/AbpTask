using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.Common.ValueObjects
{
	/// <summary>
	/// Value object describing cost of another object
	/// </summary>
	public sealed class Cost : ValueObject
	{
		public decimal Value { get; private set; }

		private Cost(decimal value)
		{
			Value = value;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static Cost Create(decimal value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Cost cannot be negative");
			}

			var rounded = Math.Round(value, 2);

			return new Cost(rounded);
		}

		public static Cost operator +(Cost a, Cost b)
		{
			return Create(a.Value + b.Value);
		}

		public static Cost operator *(Cost a, decimal b)
		{
			return Create(a.Value * b);
		}
	}
}
