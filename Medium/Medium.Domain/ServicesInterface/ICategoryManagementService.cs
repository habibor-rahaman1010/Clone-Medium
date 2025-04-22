using Medium.Domain.Entities;

namespace Medium.Domain.ServicesInterface
{
    public interface ICategoryManagementService
    {
        Task<(IList<Category> Items, int CurrentPage, int TotalPages, int TotalItems, int PageSize)> GetCategoriesAsync(int pageIndex, int pageSize);
        Task<Category> GetCategoryById(Guid id);  
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        //Task DeleteCategoryAsync(Guid id);
    }
}
