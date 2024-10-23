using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Repositories;
using SmartHall.DAL.Sql.Entities;

namespace SmartHall.DAL.Sql.Repos
{
    public sealed class HallRepository : IHallRepository
    {
        private readonly SmartHallDbContext _dbContext;
        private readonly IMapper _mapper;

        public HallRepository(SmartHallDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(Hall entity, CancellationToken cancellationToken)
        {
            var hallEntity = _mapper.Map<HallEntity>(entity);

            await _dbContext.Halls.AddAsync(hallEntity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Hall entity, CancellationToken cancellationToken)
        {
            var hallEntity = _mapper.Map<HallEntity>(entity);

            var hallToDelete = await _dbContext.Halls.FirstOrDefaultAsync(e => e == hallEntity, cancellationToken);

            _dbContext.Halls.Remove(hallToDelete);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Hall>> GetAllAsync(CancellationToken cancellationToken)
        {
            var halls =  await _dbContext.Halls.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Hall>>(halls);
        }

        public async Task<IEnumerable<Hall>> GetAllWithEquipment(CancellationToken cancellationToken)
        {
            var halls =  await _dbContext.Halls.Include(e => e.HallEquipment).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Hall>>(halls);
        }

        public async Task<Hall> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var hall = await _dbContext.Halls.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<Hall>(hall);
        }

        public async Task<Hall> GetByIdWithEquipmentAndReservations(Guid id, CancellationToken cancellationToken)
        {
            var hall = await _dbContext.Halls
                .Include(h => h.HallEquipment)
                .Include(h => h.Reservations)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<Hall>(hall);
        }

        public async Task UpdateAsync(Hall entity, CancellationToken cancellationToken)
        {
            var hallEntity = _mapper.Map<HallEntity>(entity);

            var hallToUpdate = await _dbContext.Halls.FirstOrDefaultAsync(e => e == hallEntity, cancellationToken);

            _dbContext.Halls.Update(hallToUpdate);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
