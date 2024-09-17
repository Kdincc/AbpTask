using FluentValidation;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Domain.Common.Constanst;
using SmartHall.Domain.Common.Constanst.Halls;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Halls.Validators
{
    public sealed class CreateHallRequestValidator : AbstractValidator<CreateHallRequest>
    {
        private readonly IValidator<CreateHallEquipmentDto> _equipmentValidator;

        public CreateHallRequestValidator(IValidator<CreateHallEquipmentDto> equipmentValidator)
        {
            _equipmentValidator = equipmentValidator;

            RuleFor(c => c.HallName)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLenght);

            RuleFor(c => c.Capacity)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.BaseHallCost)
                .NotEmpty()
				.GreaterThan(0);

            RuleFor(c => c.Equipment)
				.Must(ValidateHallEquipment)
				.WithMessage("One or more equipments not valid");
        }

        private bool ValidateHallEquipment(List<CreateHallEquipmentDto> equipmentDtos)
        {
            foreach (var equipment in equipmentDtos)
			{
				var validationResult = _equipmentValidator.Validate(equipment);

				if (!validationResult.IsValid)
				{
					return false;
				}
			}

            return true;
        }
	}
}
