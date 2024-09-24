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
