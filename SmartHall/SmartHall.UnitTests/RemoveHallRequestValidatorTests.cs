using FluentValidation;
using SmartHall.BLL.Halls.Validators;
using SmartHall.Common.Halls.Models.RemoveHall;

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
