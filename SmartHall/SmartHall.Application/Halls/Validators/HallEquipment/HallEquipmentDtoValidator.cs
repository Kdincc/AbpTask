using FluentValidation;
using SmartHall.Common.Halls.Models.Dtos;
using SmartHall.Common.Shared.Constants.Halls;

namespace SmartHall.BLL.Halls.Validators.HallEquipment
{
    public sealed class HallEquipmentDtoValidator : AbstractValidator<HallEquipmentDto>
    {
        public HallEquipmentDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(HallEquipmentConstants.MaxNameLength);

            RuleFor(x => x.Cost)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
