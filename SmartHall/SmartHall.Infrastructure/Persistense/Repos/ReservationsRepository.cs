using Microsoft.EntityFrameworkCore;
using SmartHall.Application.Common.Persistance;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Repos
{
	public sealed class ReservationsRepository : IReservationRepository
	{
		private readonly SmartHallDbContext _context;

		public ReservationsRepository(SmartHallDbContext dbContext) 
		{
			_context = dbContext;
		}

		public async Task AddAsync(Reservation entity, CancellationToken cancellationToken)
		{
			await _context.Reservations.AddAsync(entity, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(Reservation entity, CancellationToken cancellationToken)
		{
			var reservationToDelete = await _context.Reservations.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.Reservations.Remove(reservationToDelete);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _context.Reservations.ToListAsync(cancellationToken);
		}

		public Task<Reservation> GetByIdAsync(ReservationId id, CancellationToken cancellationToken)
		{
			var reservation = _context.Reservations.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

			return reservation;
		}

		public async Task UpdateAsync(Reservation entity, CancellationToken cancellationToken)
		{
			var reservationToUpdate = await _context.Reservations.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.Reservations.Update(reservationToUpdate);

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
