using FluentValidation;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Domain.Common.Constanst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators
{
    public sealed class ReserveHallRequestValidator : AbstractValidator<ReserveHallRequest>
    {
        public ReserveHallRequestValidator(TimeProvider timeProvider)
        {
            RuleFor(request => request)
                .Must(BeWithinOneDay)
				.WithMessage("Reservation must be within one day");

            RuleFor(c => c.HallId)
                .NotEmpty();

            RuleFor(c => c.ReservationDateTime)
                .NotEmpty()
                .Must(c => c >= timeProvider.GetUtcNow())
                .WithMessage("Reservation date time must be in the future")
                .Must(c => c.TimeOfDay >= BusinessHours.OpenTime && c.TimeOfDay <= BusinessHours.CloseTime)
                .WithMessage("Reservation time must be in range 06:00 - 23:00");

            RuleFor(c => c.Duratation)
                .NotEmpty()
                .GreaterThan(TimeSpan.Zero);
        }

        private bool BeWithinOneDay(ReserveHallRequest request)
        {
			DateTime start = request.ReservationDateTime;
			DateTime end = start.Add(request.Duratation);

			return start.Date == end.Date;
		}
    }
}
