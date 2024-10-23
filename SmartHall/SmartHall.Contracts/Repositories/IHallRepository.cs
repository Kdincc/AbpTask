using SmartHall.Common.Halls.Models.HallAggregate;

namespace SmartHall.Common.Repositories
{
    public interface IHallRepository : IRepository<Hall>
    {
        public Task<Hall> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        public Task<IEnumerable<Hall>> GetAllWithEquipment(CancellationToken cancellationToken);

        public Task<Hall> GetByIdWithEquipmentAndReservations(Guid id, CancellationToken cancellationToken);
    }
}
