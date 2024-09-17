using ErrorOr;
using SmartHall.Application.Common.Mapping;
using SmartHall.Application.Common.Persistance;
using SmartHall.Application.Halls.ReservationStrategies;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.SearchFreeHall;
using SmartHall.Contracts.Halls.UpdateHall;
using SmartHall.Domain.Common.Errors;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<HallEquipment> hallEquipment = request.Equipment.Select(e => e.FromDto()).ToList();

            Hall newHall = new(HallId.CreateUnique(), request.HallName, hallCapacity, baseCost, hallEquipment, []);

            var halls = await _repository.GetAllWithEquipment(cancellationToken);

            if (halls.Any(h => h.IsSameAs(newHall)))
			{
				return HallErrors.Dublication;
			}

            await _repository.AddAsync(newHall, cancellationToken);

            return new CreateHallResponse(newHall.Id.Value);
        }

        public async Task<ErrorOr<RemoveHallResponse>> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken)
        {
            Hall hallToDelete = await _repository.GetByIdAsync(HallId.Create(request.HallId.ToString()), cancellationToken);

            if (hallToDelete is null)
            {
                return HallErrors.HallNotFound;
            }

            await _repository.DeleteAsync(hallToDelete, cancellationToken);

            return new RemoveHallResponse(hallToDelete.Id.Value);
        }

        public async Task<ErrorOr<ReserveHallResponse>> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken)
        {
            Hall hallToReserve = await _repository.GetByIdAsync(HallId.Create(request.HallId.ToString()), cancellationToken);
            List<HallEquipment> selectedEquipment = request.SelectedEquipment.Select(e => e.FromDto()).ToList();

            if (hallToReserve is null)
            {
                return HallErrors.HallNotFound;
            }

			if (!selectedEquipment.SequenceEqual(hallToReserve.AvailableEquipment))
			{
				return HallErrors.SelectedHallEquipmentNotAvailable;
			}

			ReservationPeriod reservationPeriod = ReservationPeriod.Create(request.ReservationDateTime, request.Duratation);

            if (hallToReserve.Reservations.Any(r => r.Period.Overlapse(reservationPeriod)))
            {
                return HallErrors.HallAlreadyReserved;
            }

            Reservation reservation = new(ReservationId.CreateUnique(), reservationPeriod);

            Cost totalCost = hallToReserve.Reserve(reservation, selectedEquipment, new HallReservationStrategy());

            await _repository.UpdateAsync(hallToReserve, cancellationToken);

            return new ReserveHallResponse(totalCost.Value);
        }

		public async Task<ErrorOr<SearchFreeHallResponse>> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken)
		{
            Capacity capacity = Capacity.Create(request.Capacity);
            ReservationPeriod period = ReservationPeriod.Create(request.DateTime, request.Duratation);
            IEnumerable<Hall> halls = await _repository.GetAllAsync(cancellationToken);

            var matches = halls.Where(h => h.Capacity == capacity && !h.Reservations.All(r => !r.Period.Overlapse(period)));

            return new SearchFreeHallResponse(matches.Select(m => m.ToDto()).ToList());
		}

		public async Task<ErrorOr<UpdateHallResponse>> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken)
        {
            Hall hallToUpdate = await _repository.GetByIdAsync(HallId.Create(request.HallId.ToString()), cancellationToken);

            if (hallToUpdate is null)
            {
                return HallErrors.HallNotFound;
            }

            Cost baseCost = Cost.Create(request.BaseCost);
            Capacity hallCapacity = Capacity.Create(request.Capacity);
            List<HallEquipment> hallEquipment = request.HallEquipment.Select(e => e.FromDto()).ToList();

            hallToUpdate.Update(request.Name, hallCapacity, baseCost, hallEquipment);

            var halls = await _repository.GetAllAsync(cancellationToken);

            if (halls.Any(h => h.IsSameAs(hallToUpdate)))
			{
				return HallErrors.Dublication;
			}

            await _repository.UpdateAsync(hallToUpdate, cancellationToken);

            return new UpdateHallResponse(hallToUpdate.Id.Value);
        }
    }
}
