using FluentValidation;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Shared.Constants.Halls;

namespace SmartHall.BLL.Halls.Validators
{
    public sealed class CreateHallRequestValidator : AbstractValidator<CreateHallRequest>
    {
        private readonly IValidator<CreateHallEquipmentDto> _equipmentValidator;

        public CreateHallRequestValidator(IValidator<CreateHallEquipmentDto> equipmentValidator)
        {
            _equipmentValidator = equipmentValidator;

            RuleFor(c => c.HallName)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLength);

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
