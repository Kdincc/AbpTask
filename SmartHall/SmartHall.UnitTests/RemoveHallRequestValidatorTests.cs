using FluentValidation;
using SmartHall.Application.Halls.Validators;
using SmartHall.Contracts.Halls.RemoveHall;

namespace SmartHall.UnitTests
{
	public sealed class RemoveHallRequestValidatorTests
	{
		private readonly IValidator<RemoveHallRequest> _removeHallRequestValidator = new RemoveHallValidator();

		[Fact]
		public void InvalidResultWhenHallIdIsEmpty()
		{
			var removeHallRequest = new RemoveHallRequest(Guid.Empty);

			var result = _removeHallRequestValidator.Validate(removeHallRequest);

			Assert.False(result.IsValid);
		}
	}
}
