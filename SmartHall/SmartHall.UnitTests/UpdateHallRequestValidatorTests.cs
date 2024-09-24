using FluentValidation;
using FluentValidation.Results;
using Moq;
using SmartHall.Application.Halls.Validators;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.UpdateHall;
using SmartHall.Domain.Common.Constants.Halls;

namespace SmartHall.UnitTests
{
	public sealed class UpdateHallRequestValidatorTests
	{
		private readonly IValidator<UpdateHallRequest> _updateHallRequestValidator;
		private readonly Mock<IValidator<CreateHallEquipmentDto>> _updateHallEquipmentDtoValidator = new();

		public UpdateHallRequestValidatorTests()
		{
			_updateHallRequestValidator = new UpdateHallRequestValidator(_updateHallEquipmentDtoValidator.Object);
		}

		[Fact]
		public void InvalidResultWhenHallNameIsEmpty()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), string.Empty, 10, 10, []);

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenHallNameIsTooLong()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), new string('a', HallConstants.MaxNameLength + 1), 10, 10, []);

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenCapacityIsZero()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), "Hall", 0, 10, []);

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenBaseHallCostIsZero()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), "Hall", 10, 0, []);

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenHallEquipmentInvalid()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), "Hall", 10, 10, new List<CreateHallEquipmentDto> { new CreateHallEquipmentDto(string.Empty, 10) });

			_updateHallEquipmentDtoValidator.Setup(x => x.Validate(It.IsAny<CreateHallEquipmentDto>())).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Name", "Name is required") }));

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void ValidResultWhenDataIsValid()
		{
			var updateHallRequest = new UpdateHallRequest(Guid.NewGuid(), "Hall", 10, 10, new List<CreateHallEquipmentDto> { new CreateHallEquipmentDto("Equipment", 10) });

			_updateHallEquipmentDtoValidator.Setup(x => x.Validate(It.IsAny<CreateHallEquipmentDto>())).Returns(new ValidationResult());

			var result = _updateHallRequestValidator.Validate(updateHallRequest);

			Assert.True(result.IsValid);
		}
	}
}
