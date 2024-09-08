using Microsoft.EntityFrameworkCore;
using SmartHall.Application.Common.Persistance;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
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

		public async Task AddAsync(Domain.HallAggregate.Entities.HallEquipment.HallEquipment entity, CancellationToken cancellationToken)
		{
			await _context.AddAsync(entity, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(Domain.HallAggregate.Entities.HallEquipment.HallEquipment entity, CancellationToken cancellationToken)
		{
			var hallEquipmentTypeToDelete = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.HallEquipmentTypes.Remove(hallEquipmentTypeToDelete);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<Domain.HallAggregate.Entities.HallEquipment.HallEquipment>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _context.HallEquipmentTypes.ToListAsync(cancellationToken);
		}

		public async Task<Domain.HallAggregate.Entities.HallEquipment.HallEquipment> GetByIdAsync(Domain.HallAggregate.Entities.HallEquipment.ValueObjects.HallEquipmentId id, CancellationToken cancellationToken)
		{
			var hallEquipmentType = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

			return hallEquipmentType;
		}

		public async Task UpdateAsync(Domain.HallAggregate.Entities.HallEquipment.HallEquipment entity, CancellationToken cancellationToken)
		{
			var hallEquipmentTypeToUpdate = await _context.HallEquipmentTypes.FirstOrDefaultAsync(e => e == entity, cancellationToken);

			_context.HallEquipmentTypes.Update(hallEquipmentTypeToUpdate);

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
