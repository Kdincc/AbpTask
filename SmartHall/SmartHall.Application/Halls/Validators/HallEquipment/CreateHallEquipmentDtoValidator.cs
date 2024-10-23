using FluentValidation;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Shared.Constants.Halls;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.BLL.Halls.Validators.HallEquipment
{
    public sealed class CreateHallEquipmentDtoValidator : AbstractValidator<CreateHallEquipmentDto>
    {
        public CreateHallEquipmentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLength);

            RuleFor(x => x.Cost)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
