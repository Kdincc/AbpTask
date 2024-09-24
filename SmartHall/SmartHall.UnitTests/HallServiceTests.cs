using Moq;
using SmartHall.Application.Common.Mapping;
using SmartHall.Application.Common.Persistance;
using SmartHall.Application.Halls.Services;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.UpdateHall;
using SmartHall.Domain.Common.Errors;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;

namespace SmartHall.UnitTests
{
	public class HallServiceTests
	{
		private readonly Mock<IHallRepository> _hallRepositoryMock = new();
		private readonly IHallService _hallService;

		public HallServiceTests()
		{
			_hallService = new HallService(_hallRepositoryMock.Object);
		}

		[Fact]
		public async Task ReserveHall_ValidData_OneReservationPlan_ReturnResevationCost_ReservationAddedToHall()
		{
			// Arrange
			decimal expected = 340m;
			Guid hallId = Guid.NewGuid();
			List<HallEquipment> hallEquipment =
			[
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(20), hallId)
			];
			Hall hallToReserve = new(Guid.NewGuid(), "Hall", Capacity.Create(100), Cost.Create(100), hallEquipment, []);
			DateTime reservationDateTime = new(new DateOnly(2024, 11, 6), new TimeOnly(14, 0, 0));
			int hours = 2;
			List<HallEquipmentDto> equipmentDtos = hallEquipment.Select(e => e.ToDto()).ToList();
			ReserveHallRequest request = new(hallId, reservationDateTime, hours, equipmentDtos);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdWithEquipmentAndReservations(hallId, CancellationToken.None)).ReturnsAsync(hallToReserve);

			// Act
			var result = await _hallService.ReserveHall(request, CancellationToken.None);

			// Assert
			Assert.Equal(expected, result.Value.TotalCost);
			Assert.True(hallToReserve.Reservations.Count != 0);
		}

		[Fact]
		public async Task ReserveHall_ValidData_MultipleReservationPlan_ReturnResevationCost_ReservationAddedToHall()
		{
			// Arrange
			decimal expected = 2020m;
			Guid hallId = Guid.NewGuid();
			List<HallEquipment> hallEquipment =
			[
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId)
			];
			Hall hallToReserve = new(Guid.NewGuid(), "Hall", Capacity.Create(100), Cost.Create(100), hallEquipment, []);
			DateTime reservationDateTime = new(new DateOnly(2024, 11, 6), new TimeOnly(9, 0, 0));
			int hours = 10;
			List<HallEquipmentDto> equipmentDtos = hallEquipment.Select(e => e.ToDto()).ToList();
			ReserveHallRequest request = new(hallId, reservationDateTime, hours, equipmentDtos);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdWithEquipmentAndReservations(hallId, CancellationToken.None)).ReturnsAsync(hallToReserve);

			// Act
			var result = await _hallService.ReserveHall(request, CancellationToken.None);

			// Assert
			Assert.Equal(expected, result.Value.TotalCost);
			Assert.True(hallToReserve.Reservations.Count != 0);
		}

		[Fact]
		public async Task ReserveHall_InvalidHallId_ReturnNotFoundError()
		{
			// Arrange
			var hallId = Guid.NewGuid();
			ReserveHallRequest reserveHallRequest = new(hallId, DateTime.UtcNow, 0, []);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdWithEquipmentAndReservations(hallId, CancellationToken.None)).ReturnsAsync(It.IsAny<Hall>());

			// Act
			var result = await _hallService.ReserveHall(reserveHallRequest, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.HallNotFound);
		}

		[Fact]
		public async Task ReserveHall_SelectedEquipmentContainsNotAvailable_SelectedHallEquipmentNotAvailableError()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			List<HallEquipment> hallEquipment =
			[
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(20), hallId)
			];
			List<HallEquipment> selectedHallEquipment =
			[
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(20), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(20), hallId)
			];
			Hall hallToReserve = new(Guid.NewGuid(), "Hall", Capacity.Create(100), Cost.Create(100), hallEquipment, []);
			List<HallEquipmentDto> equipmentDtos = selectedHallEquipment.Select(e => e.ToDto()).ToList();
			ReserveHallRequest request = new(hallId, DateTime.Now, 0, equipmentDtos);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdWithEquipmentAndReservations(hallId, CancellationToken.None)).ReturnsAsync(hallToReserve);

			// Act
			var result = await _hallService.ReserveHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.SelectedHallEquipmentNotAvailable);
		}

		[Fact]
		public async Task ReserveHall_ReservationTimeOverlapsWithAnother_ReturnHallAlreadyReservedError()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			DateTime reservationDateTime = new(new DateOnly(2024, 11, 6), new TimeOnly(14, 0, 0));
			List<Reservation> reservations =
			[
				new Reservation(Guid.NewGuid(), ReservationPeriod.Create(reservationDateTime, TimeSpan.FromHours(2)), hallId)

			];
			Hall hallToReserve = new(Guid.NewGuid(), "Hall", Capacity.Create(100), Cost.Create(100), [], reservations);
			DateTime newReservationDateTime = new(new DateOnly(2024, 11, 6), new TimeOnly(15, 0, 0));

			ReserveHallRequest request = new(hallId, newReservationDateTime, 2, []);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdWithEquipmentAndReservations(hallId, CancellationToken.None)).ReturnsAsync(hallToReserve);

			// Act
			var result = await _hallService.ReserveHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.HallAlreadyReserved);
		}

		[Fact]
		public async Task CreateHall_ValidData_HallAdded_ReturnResponse()
		{
			// Arrange
			CreateHallRequest request = new("Hall", 100, [], 100);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetAllWithEquipment(CancellationToken.None)).ReturnsAsync([]);

			// Act
			var result = await _hallService.CreateHall(request, CancellationToken.None);

			// Assert
			Assert.False(result.IsError);
			_hallRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Hall>(), CancellationToken.None), Times.Once);
		}

		[Fact]
		public async Task CreateHall_HallAlreadyExists_ReturnDublicationError()
		{
			// Arrange
			CreateHallRequest request = new("Hall", 100, [], 100);
			Hall hall = new(Guid.NewGuid(), "Hall", Capacity.Create(100), Cost.Create(100), [], []);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetAllWithEquipment(CancellationToken.None)).ReturnsAsync([hall]);

			// Act
			var result = await _hallService.CreateHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.Duplication);
		}

		[Fact]
		public async Task RemoveHall_ValidHallId_HallDeleted_ReturnResponse()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			RemoveHallResponse expected = new(hallId);
			Hall hallToDelete = new(hallId, "Hall", Capacity.Create(100), Cost.Create(100), [], []);
			RemoveHallRequest request = new(hallId);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdAsync(hallId, CancellationToken.None)).ReturnsAsync(hallToDelete);

			// Act
			var result = await _hallService.RemoveHall(request, CancellationToken.None);

			// Assert
			Assert.False(result.IsError);
			_hallRepositoryMock.Verify(r => r.DeleteAsync(hallToDelete, CancellationToken.None), Times.Once);
			Assert.Equal(result.Value, expected);
		}

		[Fact]
		public async Task RemoveHall_InvalidHallId_ReturnNotFoundError()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			RemoveHallRequest request = new(hallId);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdAsync(hallId, CancellationToken.None)).ReturnsAsync(It.IsAny<Hall>());

			// Act
			var result = await _hallService.RemoveHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.HallNotFound);
		}

		[Fact]
		public async Task UpdateHall_ValidData_ReturnsResponse()
		{
			// Arrange
			var hallId = Guid.NewGuid();
			UpdateHallResponse expected = new(hallId);
			List<HallEquipment> hallEquipment =
			[
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(50), hallId),
				new HallEquipment(Guid.NewGuid(), "eqipment", Cost.Create(20), hallId)
			];
			List<CreateHallEquipmentDto> newEquipment =
			[
				new CreateHallEquipmentDto("eqipment", 50),
				new CreateHallEquipmentDto("eqipment", 20)
			];
			Hall hallToUpdate = new(hallId, "Hall", Capacity.Create(100), Cost.Create(100), hallEquipment, []);
			UpdateHallRequest request = new(hallId, "NewHall", 200, 200, newEquipment);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdAsync(hallId, CancellationToken.None)).ReturnsAsync(hallToUpdate);


			// Act
			var result = await _hallService.UpdateHall(request, CancellationToken.None);

			// Assert
			Assert.False(result.IsError);
			_hallRepositoryMock.Verify(r => r.UpdateAsync(hallToUpdate, CancellationToken.None), Times.Once);
			Assert.Equal(result.Value, expected);
		}

		[Fact]
		public async Task UpdateHall_InvalidHallId_ReturnNotFoundError()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			UpdateHallRequest request = new(hallId, "NewHall", 200, 200, []);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetByIdAsync(hallId, CancellationToken.None)).ReturnsAsync(It.IsAny<Hall>());

			// Act
			var result = await _hallService.UpdateHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.HallNotFound);
		}

		[Fact]
		public async Task UpdateHall_HallAlreadyExists_ReturnDublicationError()
		{
			// Arrange
			Guid hallId = Guid.NewGuid();
			UpdateHallRequest request = new(hallId, "NewHall", 200, 200, []);
			Hall hall = new(hallId, "Hall", Capacity.Create(100), Cost.Create(50), [], []);
			Hall exsitedHall = new(Guid.NewGuid(), request.Name, Capacity.Create(request.Capacity), Cost.Create(request.BaseCost), [], []);

			// Setup
			_hallRepositoryMock.Setup(r => r.GetAllWithEquipment(CancellationToken.None)).ReturnsAsync([exsitedHall]);
			_hallRepositoryMock.Setup(r => r.GetByIdAsync(hallId, CancellationToken.None)).ReturnsAsync(hall);

			// Act
			var result = await _hallService.UpdateHall(request, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal(result.FirstError, HallErrors.Duplication);
		}

		[Fact]
		public async Task SearchFreeHall_ValidData_ReturnsResponse_WithHall()
		{
			// Arrange
			DateTime dateTime = new(new DateOnly(2024, 11, 6), new TimeOnly(14, 0, 0));
			SearchFreeHallRequest request = new(dateTime, 3, 50);
			Reservation firstReservation = new(Guid.NewGuid(), ReservationPeriod.Create(dateTime, TimeSpan.FromHours(2)), Guid.NewGuid());
			Reservation secondReservation = new(Guid.NewGuid(), ReservationPeriod.Create(dateTime.AddHours(4), TimeSpan.FromHours(2)), Guid.NewGuid());
			List<Hall> halls =
			[
				new Hall(Guid.NewGuid(), "Hall", Capacity.Create(50), Cost.Create(100), [], [firstReservation]),
				new Hall(Guid.NewGuid(), "Hall", Capacity.Create(50), Cost.Create(100), [], [secondReservation])
			];
			List<Guid> expected = [halls[1].Id];

			// Setup
			_hallRepositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(halls);

			// Act
			var result = await _hallService.SearchFreeHall(request, CancellationToken.None);
			var actual = result.AvailableHalls.Select(h => h.Id).ToList();

			// Assert
			Assert.True(result.AvailableHalls.Count == 1);
			Assert.Equal(actual, expected);
		}
	}
}