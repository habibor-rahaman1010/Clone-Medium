using Medium.Domain.Entities;

namespace Medium.Domain.RepositoriesInterface
{
    public interface ICategoryRepository : IRepository<Category,  Guid>
    {
    }
}
