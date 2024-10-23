using FluentValidation;
using SmartHall.Common.Authentication.Models;

namespace SmartHall.BLL.Authentication.Validation
{
    public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotEmpty();
        }
    }
}
