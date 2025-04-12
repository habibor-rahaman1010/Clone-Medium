using Medium.Domain.Entities;
using Medium.Domain.RepositoriesInterface;
using Medium.Infrastructure.Data;

namespace Medium.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly MediumDbContext _mediumDbContext;

        public CategoryRepository(MediumDbContext dbContext) : base(dbContext)
        {
            _mediumDbContext = dbContext;
        }
 
    }
}
