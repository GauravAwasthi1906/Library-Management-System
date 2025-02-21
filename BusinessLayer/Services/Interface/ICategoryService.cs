using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface ICategoryService
    {
        Task<ServiceResponse> AddNewCategory(Category category);
        Task<ServiceResponse> UpdateCategory(int? id ,Category category);
        Task<ServiceResponse> DeleteCategory(int? id);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int? id);
    }
}
