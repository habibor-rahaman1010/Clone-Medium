using Medium.Domain.Entities;

namespace Medium.Domain.RepositoriesInterface
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey> 
        where TKey : IComparable<TKey>
    {
        public Task<(IList<TEntity> items, int currentPage, int totalPages, int totalItems, int pageSize)> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TKey id, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
