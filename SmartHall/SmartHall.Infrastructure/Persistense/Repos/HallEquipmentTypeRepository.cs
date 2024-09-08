using Microsoft.EntityFrameworkCore;
using SmartHall.Application.Common.Persistance;
using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregateType;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Repos
{
	public sealed class HallEquipmentTypeRepository : IHallEquipmentTypeRepository
	{
		private readonly SmartHallDbContext _context;

		public HallEquipmentTypeRepository(SmartHallDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(HallEquipmentType entity, CancellationToken cancellationToken)
		{
			await _context.AddAsync(entity, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(HallEquipmentType entity, CancellationToken cancellationToken)
		{
			var hallEquipmentTypeToDelete = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.HallEquipmentTypes.Remove(hallEquipmentTypeToDelete);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<HallEquipmentType>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _context.HallEquipmentTypes.ToListAsync(cancellationToken);
		}

		public async Task<HallEquipmentType> GetByIdAsync(HallEquipmentTypeId id, CancellationToken cancellationToken)
		{
			var hallEquipmentType = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

			return hallEquipmentType;
		}

		public async Task UpdateAsync(HallEquipmentType entity, CancellationToken cancellationToken)
		{
			var hallEquipmentTypeToUpdate = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.HallEquipmentTypes.Update(hallEquipmentTypeToUpdate);

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
