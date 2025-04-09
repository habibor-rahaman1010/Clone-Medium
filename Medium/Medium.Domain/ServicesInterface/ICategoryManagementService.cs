using Medium.Domain.Entities;

namespace Medium.Domain.ServicesInterface
{
    public interface ICategoryManagementService
    {
        /*Task<IList<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryById(Guid id);*/       
        Task AddCategoryAsync(Category category);
      /*  Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(Category category);*/
    }
}
