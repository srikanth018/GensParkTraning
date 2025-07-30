using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<int, Category> _categoryRepo;

        public CategoryService(IRepository<int, Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<Category> DeleteCategory(int id)
        {
            var category = await _categoryRepo.Get(id);
            if (category == null)
                throw new KeyNotFoundException("Category not available for this ID");
            return await _categoryRepo.Delete(category.CategoryId);
        }

        public async Task<Category> EditCategory(int id, EditCategoryDTO editCategoryDTO)
        {
            var category = await _categoryRepo.Get(id);
            if (category == null)
                throw new KeyNotFoundException("Category not available for this ID");
            category.Name = editCategoryDTO.Name;
            return await _categoryRepo.Update(id, category);
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepo.Get(id);
            if (category == null)
                throw new KeyNotFoundException("Category not available for this ID");
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            var categoryList = await _categoryRepo.GetAll();
            if (categoryList.Count() == 0 || categoryList == null)
                throw new ArgumentException("No Categories Found");
            return categoryList;
        }

        public async Task<IEnumerable<Category>> GetCategoryListByPage(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var categoryList = await _categoryRepo.GetAll();

            if (categoryList == null || !categoryList.Any())
                throw new ArgumentException("No Categories Found");

            var paged = categoryList
                .OrderByDescending(c => c.CategoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return paged;

        }
    }
}