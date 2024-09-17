using FluentValidation;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Domain.Common.Constanst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
				.WithMessage("Reservation date time must be in the future")
				.Must(c => c.TimeOfDay >= BusinessHours.OpenTime && c.TimeOfDay <= BusinessHours.CloseTime)
				.WithMessage("Reservation time must be in range 06:00 - 23:00");

			RuleFor(c => c.Hours)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(c => c.Capacity)
				.NotEmpty()
				.GreaterThan(0);
		}

		private bool BeWithinOneDay(SearchFreeHallRequest request)
		{
			DateTime start = request.DateTime;
			DateTime end = start.Add(TimeSpan.FromHours(request.Hours));

			return start.Date == end.Date;
		}
	}
}
