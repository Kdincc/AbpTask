using FluentValidation;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Domain.Common.Constants;

namespace SmartHall.Application.Halls.Validators
{
	public sealed class SearchFreeHallRequestValidator : AbstractValidator<SearchFreeHallRequest>
	{
		public SearchFreeHallRequestValidator(TimeProvider timeProvider)
		{
			RuleFor(c => c)
				.Must(BeWithinOneDay)
				.WithMessage("Reservation must be within one day");

			RuleFor(c => c.DateTime)
				.NotEmpty()
				.Must(c => c >= timeProvider.GetUtcNow())
				.WithMessage("Reservation date time must be in the future");

			RuleFor(request => request)
				.Must(ValidateReservationTime)
				.WithMessage("Reservation time invalid. Reservation must be in range 6:00 - 23:00 and reservation must be in one calendar day");

			RuleFor(c => c.Hours)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(c => c.Capacity)
				.NotEmpty()
				.GreaterThan(0);
		}

		private bool ValidateReservationTime(SearchFreeHallRequest reserveHall)
		{
			DateTime startDate = reserveHall.DateTime;
			DateTime endDate = reserveHall.DateTime.Add(TimeSpan.FromHours(reserveHall.Hours));

			if (startDate.TimeOfDay < BusinessHours.OpenTime || endDate.TimeOfDay > BusinessHours.CloseTime)
			{
				return false;
			}

			if (endDate <= startDate)
			{
				return false;
			}

			if (startDate.Date != endDate.Date)
			{
				return false;
			}

			return true;
		}

		private bool BeWithinOneDay(SearchFreeHallRequest request)
		{
			DateTime start = request.DateTime;
			DateTime end = start.Add(TimeSpan.FromHours(request.Hours));

			return start.Date == end.Date;
		}
	}
}
