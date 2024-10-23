using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.Common.Shared.Constants.Halls
{
    public static class ReservationPlans
	{
		public readonly static ReservationPlan MorningPlan = ReservationPlan.Create(new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), 0.9m);

		public readonly static ReservationPlan PeakPlan = ReservationPlan.Create(new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0), 1.15m);

		public readonly static ReservationPlan EveningPlan = ReservationPlan.Create(new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), 0.8m);

		public readonly static ReservationPlan Default = ReservationPlan.Create(new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1m);
	}
}
