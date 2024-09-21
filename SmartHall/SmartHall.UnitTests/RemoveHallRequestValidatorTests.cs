using FluentValidation;
using SmartHall.Application.Halls.Validators;
using SmartHall.Contracts.Halls.RemoveHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
