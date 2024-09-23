using ErrorOr;
using SmartHall.Application.Common.Mapping;
using SmartHall.Application.Common.Persistance;
using SmartHall.Application.Halls.ReservationStrategies;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.SearchFreeHall;
using SmartHall.Contracts.Halls.UpdateHall;
using SmartHall.Domain.Common.Errors;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;

namespace SmartHall.Application.Halls.Services
{
	public sealed class HallService : IHallService
	{
		private readonly IHallRepository _repository;

		public HallService(IHallRepository hallRepository)
		{
			_repository = hallRepository;
		}

		public async Task<ErrorOr<CreateHallResponse>> CreateHall(CreateHallRequest request, CancellationToken cancellationToken)
		{
			Cost baseCost = Cost.Create(request.BaseHallCost);
			Capacity hallCapacity = Capacity.Create(request.Capacity);
			Guid id = Guid.NewGuid();
			List<HallEquipment> hallEquipment = request.Equipment.Select(e => e.FromDto(id)).ToList();

			Hall newHall = new(id, request.HallName, hallCapacity, baseCost, hallEquipment, []);

			var halls = await _repository.GetAllWithEquipment(cancellationToken);

			if (halls.Any(h => h.IsSameAs(newHall)))
			{
				return HallErrors.Duplication;
			}

			await _repository.AddAsync(newHall, cancellationToken);

			return new CreateHallResponse(newHall.Id);
		}

		public async Task<ErrorOr<RemoveHallResponse>> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken)
		{
			Hall hallToDelete = await _repository.GetByIdAsync(request.HallId, cancellationToken);

			if (hallToDelete is null)
			{
				return HallErrors.HallNotFound;
			}

			await _repository.DeleteAsync(hallToDelete, cancellationToken);

			return new RemoveHallResponse(hallToDelete.Id);
		}

		public async Task<ErrorOr<ReserveHallResponse>> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken)
		{
			Hall hallToReserve = await _repository.GetByIdWithEquipmentAndReservations(request.HallId, cancellationToken);
			List<HallEquipment> selectedEquipment = request.SelectedEquipment.Select(e => e.FromDto()).ToList();
			TimeSpan duration = TimeSpan.FromHours(request.Hours);

			if (hallToReserve is null)
			{
				return HallErrors.HallNotFound;
			}

			if (hallToReserve.HasNotAvailableEquipment(selectedEquipment))
			{
				return HallErrors.SelectedHallEquipmentNotAvailable;
			}

			ReservationPeriod reservationPeriod = ReservationPeriod.Create(request.ReservationDateTime, duration);
			Reservation reservation = new(Guid.NewGuid(), reservationPeriod, request.HallId);

			if (hallToReserve.IsReservationOverlaps(reservation))
			{
				return HallErrors.HallAlreadyReserved;
			}

			Cost totalCost = hallToReserve.Reserve(reservation, selectedEquipment, new HallReservationStrategy());

			await _repository.UpdateAsync(hallToReserve, cancellationToken);

			return new ReserveHallResponse(totalCost.Value);
		}

		public async Task<SearchFreeHallResponse> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken)
		{
			TimeSpan duration = TimeSpan.FromHours(request.Hours);
			Capacity capacity = Capacity.Create(request.Capacity);
			ReservationPeriod period = ReservationPeriod.Create(request.DateTime, duration);

			IEnumerable<Hall> halls = await _repository.GetAllAsync(cancellationToken);

			var matches = halls.Where(h => h.Capacity == capacity && h.Reservations.All(r => !r.Period.Overlaps(period))).ToList();

			return new SearchFreeHallResponse(matches.Select(m => m.ToDto()).ToList());
		}

		public async Task<ErrorOr<UpdateHallResponse>> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken)
		{
			Hall hallToUpdate = await _repository.GetByIdAsync(request.HallId, cancellationToken);

			if (hallToUpdate is null)
			{
				return HallErrors.HallNotFound;
			}

			Cost baseCost = Cost.Create(request.BaseCost);
			Capacity hallCapacity = Capacity.Create(request.Capacity);
			List<HallEquipment> hallEquipment = request.HallEquipment.Select(e => e.FromDto(hallToUpdate.Id)).ToList();

			hallToUpdate.Update(request.Name, hallCapacity, baseCost, hallEquipment);

			var halls = await _repository.GetAllWithEquipment(cancellationToken);

			if (halls.Any(h => h.IsSameAs(hallToUpdate)))
			{
				return HallErrors.Duplication;
			}

			await _repository.UpdateAsync(hallToUpdate, cancellationToken);

			return new UpdateHallResponse(hallToUpdate.Id);
		}
	}
}
