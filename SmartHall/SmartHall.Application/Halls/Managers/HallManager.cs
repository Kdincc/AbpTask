using ErrorOr;
using SmartHall.Application.Common.Mapping;
using SmartHall.BLL.Halls.HallAggregate.Entities.Reservation;
using SmartHall.BLL.Halls.HallAggregate.ValueObjects;
using SmartHall.BLL.Halls.ReservationStrategies;
using SmartHall.Common.Halls;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Common.Halls.Models.RemoveHall;
using SmartHall.Common.Halls.Models.ReserveHall;
using SmartHall.Common.Halls.Models.SearchFreeHall;
using SmartHall.Common.Halls.Models.UpdateHall;
using SmartHall.Common.Repositories;
using SmartHall.Common.Shared.Errors;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.BLL.Halls.Managers
{
    /// <summary>
    /// Service for hall operations
    /// </summary>
    public sealed class HallService : IHallManager
    {
        private readonly IHallRepository _repository;

        public HallService(IHallRepository hallRepository)
        {
            _repository = hallRepository;
        }

        /// <summary>
        /// Method for creating a new hall
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>CreateHallResponse with id of new hall, if hall with same properties already exists returns return HallErrors.Duplication </returns>
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

        /// <summary>
        /// Remove hall by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>RemoveHallResponse with id of removed hall, if hall with given id not found returns HallErrors.HallNotFound</returns>
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

        /// <summary>
        /// Reserve hall with selected equipment, start date and duration
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>ReserveHallResponse with total cost of reservation. 
        /// If hall by given id not found returns HallErrors.HallNotFound. 
        /// If selected equipment contains not available for this hall returns HallErrors.SelectedHallEquipmentNotAvailable.
        /// If new reservation overlaps with another returns HallErrors.HallAlreadyReserved</returns>
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

        /// <summary>
        /// Search for halls with given capacity and start date and duration
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>SearchFreeHallResponse with collection of matches</returns>
        public async Task<SearchFreeHallResponse> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken)
        {
            TimeSpan duration = TimeSpan.FromHours(request.Hours);
            Capacity capacity = Capacity.Create(request.Capacity);
            ReservationPeriod period = ReservationPeriod.Create(request.DateTime, duration);

            IEnumerable<Hall> halls = await _repository.GetAllAsync(cancellationToken);

            var matches = halls.Where(h => h.Capacity == capacity && h.Reservations.All(r => !r.Period.Overlaps(period))).ToList();

            return new SearchFreeHallResponse(matches.Select(m => m.ToDto()).ToList());
        }

        /// <summary>
        /// Updates hall with given id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>UpdateHallResponse with id of update hall.
        /// If hall after update same as existed in repository returns return HallErrors.Duplication. 
        /// If hall with given id not found returns HallErrors.HallNotFound </returns>
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
