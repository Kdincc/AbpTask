using FluentValidation;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Domain.Common.Constanst;
using SmartHall.Domain.Common.Constanst.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators
{
    public sealed class CreateHallRequestValidator : AbstractValidator<CreateHallRequest>
    {
        public CreateHallRequestValidator()
        {
            RuleFor(c => c.HallName)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLenght);

            RuleFor(c => c.Capacity)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
