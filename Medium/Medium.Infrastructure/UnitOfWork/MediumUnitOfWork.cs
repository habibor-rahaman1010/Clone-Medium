using Medium.Domain.RepositoriesInterface;
using Medium.Domain.UnitOfWorkInterface;
using Medium.Infrastructure.Data;

namespace Medium.Infrastructure.UnitOfWork
{
    public class MediumUnitOfWork : UnitOfWork, IMediumUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; private set; }

        public MediumUnitOfWork(MediumDbContext dbContext, 
            ICategoryRepository categoryRepository) : base(dbContext)
        {
            CategoryRepository = categoryRepository;
        }

    }
}
