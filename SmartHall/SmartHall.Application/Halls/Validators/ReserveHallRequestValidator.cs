using FluentValidation;
using SmartHall.Contracts.Halls.ReserveHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators
{
    public sealed class ReserveHallRequestValidator : AbstractValidator<ReserveHallRequest>
    {
        public ReserveHallRequestValidator()
        {
            RuleFor(c => c.HallId)
                .NotEmpty();

            RuleFor(c => c.ReservationDateTime)
                .NotEmpty();

            RuleFor(c => c.Duratation)
                .NotEmpty();
        }
    }
}
