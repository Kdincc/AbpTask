using FluentValidation;
using SmartHall.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
