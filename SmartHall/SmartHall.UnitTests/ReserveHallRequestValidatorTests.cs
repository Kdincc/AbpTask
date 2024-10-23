using FluentValidation;
using FluentValidation.Results;
using Moq;
using SmartHall.BLL.Halls.Validators;
using SmartHall.Common.Halls.Models.Dtos;
using SmartHall.Common.Halls.Models.ReserveHall;

namespace SmartHall.UnitTests
{
    public sealed class ReserveHallRequestValidatorTests
	{
		private readonly IValidator<ReserveHallRequest> _reserveHallRequestValidator;
		private readonly Mock<TimeProvider> _timeProviderMock = new();
		private readonly Mock<IValidator<HallEquipmentDto>> _hallEquipmentDtoMock = new();

		public ReserveHallRequestValidatorTests()
		{
			_reserveHallRequestValidator = new ReserveHallRequestValidator(_timeProviderMock.Object, _hallEquipmentDtoMock.Object);
		}

		[Fact]
		public void InvalidResultWhenReservationNotWithinOneDay()
		{
			var reserveHallRequest = new ReserveHallRequest(Guid.NewGuid(), DateTime.UtcNow.AddDays(1), 24, []);

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenHallIdIsEmpty()
		{
			var reserveHallRequest = new ReserveHallRequest(Guid.Empty, DateTime.UtcNow, 1, []);

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenReservationDateTimeIsInThePast()
		{
			var reserveHallRequest = new ReserveHallRequest(Guid.NewGuid(), DateTime.UtcNow.AddHours(-1), 1, []);

			_timeProviderMock.Setup(x => x.GetUtcNow()).Returns(DateTime.UtcNow);

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenReservationTimeInNotBuisnessHours()
		{
			DateTime startDate = DateTime.Parse("2024-11-12T05:00");
			TimeSpan duration = TimeSpan.FromHours(-1);
			var reserveHallRequest = new ReserveHallRequest(Guid.NewGuid(), startDate, 1, []);

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenHoursIsZero()
		{
			var reserveHallRequest = new ReserveHallRequest(Guid.NewGuid(), DateTime.UtcNow, 0, []);

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenEquipmentIsInvalid()
		{
			var reserveHallRequest = new ReserveHallRequest(Guid.NewGuid(), DateTime.UtcNow, 1, [new HallEquipmentDto(Guid.NewGuid(), "Equipment", 0, Guid.NewGuid())]);

			_hallEquipmentDtoMock.Setup(x => x.Validate(It.IsAny<HallEquipmentDto>())).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Name", "Name is required") }));

			var result = _reserveHallRequestValidator.Validate(reserveHallRequest);

			Assert.False(result.IsValid);
		}

	}
}
