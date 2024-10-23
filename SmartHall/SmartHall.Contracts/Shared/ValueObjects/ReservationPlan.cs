using SmartHall.Common.BaseModels;

namespace SmartHall.Common.Shared.ValueObjects
{
    /// <summary>
    /// Reservation plan value object, describing reservation plan for hall
    /// </summary>
    public sealed class ReservationPlan : ValueObject
    {
        public TimeSpan Start { get; private set; }

        public TimeSpan End { get; private set; }

        public decimal Modifier { get; private set; }

        private ReservationPlan(TimeSpan start, TimeSpan end, decimal modyfier)
        {
            Start = start;
            End = end;
            Modifier = modyfier;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }

        public static ReservationPlan Create(TimeSpan start, TimeSpan end, decimal modifier)
        {
            if (start >= end)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            if (modifier < 0)
            {
                throw new ArgumentException("Modifier cannot be negative");
            }

            return new ReservationPlan(start, end, modifier);
        }

        /// <summary>
        /// Method for checking if given time is within reservation plan
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Result of comparing</returns>
        public bool IsWithin(TimeSpan time)
        {
            return time >= Start && time < End;
        }
    }
}
