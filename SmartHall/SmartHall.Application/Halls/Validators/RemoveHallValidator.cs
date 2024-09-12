using FluentValidation;
using SmartHall.Contracts.Halls.RemoveHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators
{
    public sealed class RemoveHallValidator : AbstractValidator<RemoveHallRequest>
    {
        public RemoveHallValidator()
        {
            RuleFor(c => c.HallId)
                .NotEmpty();
        }
    }
}
