﻿using Microsoft.EntityFrameworkCore;
using SmartHall.Application.Common.Persistance;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Repos
{
	public sealed class HallRepository : IHallRepository
	{
		private readonly SmartHallDbContext _dbContext;

        public HallRepository(SmartHallDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Hall entity, CancellationToken cancellationToken)
		{
			await _dbContext.Halls.AddAsync(entity, cancellationToken);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(Hall entity, CancellationToken cancellationToken)
		{
			var hallToDelete = await _dbContext.Halls.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_dbContext.Halls.Remove(hallToDelete);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<Hall>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _dbContext.Halls.AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Hall>> GetAllWithEquipment(CancellationToken cancellationToken)
		{
			return await _dbContext.Halls.Include(e => e.AvailableEquipment).AsNoTracking().ToListAsync(cancellationToken);
		}

		public Task<Hall> GetByIdAsync(HallId id, CancellationToken cancellationToken)
		{
			var hall = _dbContext.Halls.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

			return hall;
		}

		public async Task UpdateAsync(Hall entity, CancellationToken cancellationToken)
		{
			var hallToUpdate = await _dbContext.Halls.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_dbContext.Halls.Update(hallToUpdate);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
