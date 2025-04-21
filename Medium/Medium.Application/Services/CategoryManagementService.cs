using Medium.Domain.Entities;
using Medium.Domain.ServicesInterface;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Services
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;

        public CategoryManagementService(IMediumUnitOfWork mediumUnitOfWork)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _mediumUnitOfWork.CategoryRepository.AddAsync(category);
            await _mediumUnitOfWork.SaveAsync();
        }

        public async Task<(IList<Category> Items, int CurrentPage, int TotalPages, int TotalItems, int PageSize)> GetCategoriesAsync(int pageIndex, int pageSize)
        {
            return await _mediumUnitOfWork.CategoryRepository.GetAllAsync(pageIndex, pageSize);
        }
    }
}
