using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Common.Persistance
{
	public interface IRepository<T>
	{
		public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

		public Task DeleteAsync(T entity, CancellationToken cancellationToken);

		public Task UpdateAsync(T entity, CancellationToken cancellationToken);

		public Task AddAsync(T entity, CancellationToken cancellationToken);
	}
}
