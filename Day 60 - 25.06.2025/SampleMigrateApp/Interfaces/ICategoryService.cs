using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryList();
        Task<IEnumerable<Category>> GetCategoryListByPage(int? page);
        Task<Category> GetCategoryById(int id);
        Task<Category> EditCategory(int id, EditCategoryDTO editCategoryDTO);
        Task<Category> DeleteCategory(int id);
        Task<Category> CreateCategory(string name);
    }
}