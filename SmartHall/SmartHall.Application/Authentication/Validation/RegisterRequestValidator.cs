using FluentValidation;
using SmartHall.Contracts.Authentication;

namespace SmartHall.Application.Authentication.Validation
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
