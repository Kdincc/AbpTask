using FluentValidation;
using SmartHall.Contracts.Halls.RemoveHall;

namespace SmartHall.Application.Halls.Validators
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
