using Medium.Domain.Entities;
using Medium.Domain.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Medium.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query =  _dbSet.AsQueryable<TEntity>();

            var entityToDelete = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (entityToDelete != null)
            {
                await DeleteAsync(entityToDelete, cancellationToken);
            }
        }

        public virtual async Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if(_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }, cancellationToken);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return await query.ToListAsync(cancellationToken);  
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            var item = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (item != null)
            {
                return item;
            }
            return item;
        }

        public virtual async Task UpdateAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            var entityToUpdate = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (entityToUpdate != null)
            {
                await UpdateAsync(entityToUpdate, cancellationToken);
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Entry(entity).State = EntityState.Modified;
            }, cancellationToken);
        }


        /*public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Update(entity);
            });
        }*/
    }
}
