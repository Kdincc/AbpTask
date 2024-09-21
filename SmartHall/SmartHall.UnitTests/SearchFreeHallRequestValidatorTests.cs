using FluentValidation;
using Moq;
using SmartHall.Application.Halls.Validators;
using SmartHall.Contracts.Halls.GetFreeHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.UnitTests
{
	public sealed class SearchFreeHallRequestValidatorTests
	{
		private readonly IValidator<SearchFreeHallRequest> _searchFreeHallRequestValidator;
		private readonly Mock<TimeProvider> _timeProviderMock = new();

		public SearchFreeHallRequestValidatorTests()
		{
			_searchFreeHallRequestValidator = new SearchFreeHallRequestValidator(_timeProviderMock.Object);
		}

		[Fact]
		public void InvalidResultWhenReservationNotWithinOneDay()
		{
			var searchFreeHallRequest = new SearchFreeHallRequest(DateTime.UtcNow.AddDays(1), 24, 10);

			var result = _searchFreeHallRequestValidator.Validate(searchFreeHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenReservationDateTimeIsInThePast()
		{
			var searchFreeHallRequest = new SearchFreeHallRequest(DateTime.UtcNow.AddHours(-1), 1, 10);

			_timeProviderMock.Setup(x => x.GetUtcNow()).Returns(DateTime.UtcNow);

			var result = _searchFreeHallRequestValidator.Validate(searchFreeHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenReservationTimeInNotBuisnessHours()
		{
			DateTime dateTime = DateTime.Parse("2024-11-12T05:00");

			var searchFreeHallRequest = new SearchFreeHallRequest(dateTime, 1, 10);

			var result = _searchFreeHallRequestValidator.Validate(searchFreeHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenDurationIsZero()
		{
			var searchFreeHallRequest = new SearchFreeHallRequest(DateTime.UtcNow, 0, 10);

			var result = _searchFreeHallRequestValidator.Validate(searchFreeHallRequest);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void InvalidResultWhenCapacityIsZero()
		{
			var searchFreeHallRequest = new SearchFreeHallRequest(DateTime.UtcNow, 1, 0);

			var result = _searchFreeHallRequestValidator.Validate(searchFreeHallRequest);

			Assert.False(result.IsValid);
		}
	}
}
