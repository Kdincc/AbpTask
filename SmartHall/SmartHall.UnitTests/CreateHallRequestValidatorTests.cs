using FluentValidation;
using Moq;
using SmartHall.Application.Halls.Validators;
using SmartHall.Domain.Common.Constants.Halls;
using FluentValidation.Results;
using SmartHall.Contracts.Halls.CreateHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.UnitTests
{
	public sealed class CreateHallRequestValidatorTests
	{
		private readonly IValidator<CreateHallRequest> _createHallRequestValidator;
		private readonly Mock<IValidator<CreateHallEquipmentDto>> _createHallEquipmentDtoValidator = new();

		public CreateHallRequestValidatorTests()
		{

			_createHallRequestValidator = new CreateHallRequestValidator(_createHallEquipmentDtoValidator.Object);
		}

		[Fact]
		public void InvalidResultWhenHallNameIsEmpty()
		{
			var createHallRequest = new CreateHallRequest(string.Empty, 10, [], 10);

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenHallNameIsTooLong()
		{
			var createHallRequest = new CreateHallRequest(new string('a', HallConstants.MaxNameLength + 1), 10, [], 10);

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenCapacityIsZero()
		{
			var createHallRequest = new CreateHallRequest("Hall", 0, [], 10);

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenBaseHallCostIsZero()
		{
			var createHallRequest = new CreateHallRequest("Hall", 10, [], 0);

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenEquipmentIsInvalid()
		{
			var createHallRequest = new CreateHallRequest("Hall", 10, [new CreateHallEquipmentDto("Equipment", 0)], 10);

			_createHallEquipmentDtoValidator.Setup(x => x.Validate(It.IsAny<CreateHallEquipmentDto>())).Returns(new ValidationResult([new ValidationFailure()]));

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void ValidResult()
		{
			var createHallRequest = new CreateHallRequest("Hall", 10, [new CreateHallEquipmentDto("Equipment", 10)], 10);

			_createHallEquipmentDtoValidator.Setup(x => x.Validate(It.IsAny<CreateHallEquipmentDto>())).Returns(new ValidationResult());

			var result = _createHallRequestValidator.Validate(createHallRequest);

			Assert.True(result.IsValid);
		}

	}
}
