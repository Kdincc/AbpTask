using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.Common.ValueObjects
{
	public sealed class ReservationPlan : ValueObject
	{
		public TimeSpan Start { get; private set; }

		public TimeSpan End { get; private set; }

		public decimal Modyfier { get; private set; }

		private ReservationPlan(TimeSpan start, TimeSpan end, decimal modyfier)
		{
			Start = start;
			End = end;
			Modyfier = modyfier;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Start;
			yield return End;
		}

		public static ReservationPlan Create(TimeSpan start, TimeSpan end, decimal modyfier)
		{
			if (start >= end)
			{
				throw new ArgumentException("Start time must be before end time");
			}

			if (modyfier < 0)
			{
				throw new ArgumentException("Modyfier cannot be negative");
			}

			return new ReservationPlan(start, end, modyfier);
		}

		public bool IsWithin(TimeSpan time)
		{
			return time >= Start && time < End;
		}
	}
}
