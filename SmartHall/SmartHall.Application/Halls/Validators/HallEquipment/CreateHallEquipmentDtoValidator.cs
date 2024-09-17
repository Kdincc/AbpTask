using FluentValidation;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Domain.Common.Constanst.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators.HallEquipment
{
    public sealed class CreateHallEquipmentDtoValidator : AbstractValidator<CreateHallEquipmentDto>
    {
        public CreateHallEquipmentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLenght);

            RuleFor(x => x.Cost)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
