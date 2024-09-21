using SmartHall.Domain.Common.Models;

namespace SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects
{
	public sealed class ReservationPeriod : ValueObject
	{
		private ReservationPeriod(DateTime start, TimeSpan duratation)
		{
			Start = start;

			Duration = duratation;

			End = start.Add(duratation);
		}

		public DateTime Start { get; private set; }

		public DateTime End { get; private set; }

		public TimeSpan Duration { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Start;
			yield return End;
		}

		public bool Overlaps(ReservationPeriod period)
		{
			return Start < period.End && End > period.Start;
		}

		public static ReservationPeriod Create(DateTime start, TimeSpan duratation)
		{
			ValidatePeriod(start, duratation);

			return new ReservationPeriod(start, duratation);
		}

		private static void ValidatePeriod(DateTime startDate, TimeSpan duration)
		{
			var endDate = startDate.Add(duration);
			var startTime = startDate.TimeOfDay;
			var endTime = endDate.TimeOfDay;

			if (startTime < TimeSpan.FromHours(6) || endTime > TimeSpan.FromHours(23))
			{
				throw new ArgumentException("Reservation must be within the operating hours of 06:00 to 23:00.");
			}

			if (endDate <= startDate)
			{
				throw new ArgumentException("End date must be after the start date.");
			}

			// Ensure that start and end times are within the same calendar day
			if (startDate.Date != endDate.Date)
			{
				throw new ArgumentException("Reservation must be within a single calendar day.");
			}
		}

	}
}
