using FluentValidation;
using SmartHall.BLL.Authentication.Validation;
using SmartHall.Common.Authentication.Models;

namespace SmartHall.UnitTests
{
    public sealed class RegisterRequestVaalidatorTests
	{
		private readonly IValidator<RegisterRequest> _registerRequestValidator;

		public RegisterRequestVaalidatorTests()
		{
			_registerRequestValidator = new RegisterRequestValidator();
		}

		[Fact]
		public void InvalidResultWhenEmailIsEmpty()
		{
			var registerRequest = new RegisterRequest(string.Empty, "password");

			var result = _registerRequestValidator.Validate(registerRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenEmailIsNotValid()
		{
			var registerRequest = new RegisterRequest("email", "password");

			var result = _registerRequestValidator.Validate(registerRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenPasswordIsEmpty()
		{
			var registerRequest = new RegisterRequest("test@example.com", string.Empty);

			var result = _registerRequestValidator.Validate(registerRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void ValidResultWhenEmailAndPasswordAreValid()
		{
			var registerRequest = new RegisterRequest("test@example.com", "password");

			var result = _registerRequestValidator.Validate(registerRequest);

			Assert.True(result.IsValid);
		}
	}
}
