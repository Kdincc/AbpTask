using FluentValidation;
using SmartHall.Common.Halls.Models.RemoveHall;

namespace SmartHall.BLL.Halls.Validators
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
