﻿using FluentValidation;
using FluentValidation.Results;
using Moq;
using SmartHall.BLL.Halls.Validators;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Shared.Constants.Halls;

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
