using SampleMigrateApp.Models;

namespace SampleMigrateApp.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetPagedProductsAsync(int? categoryId, int page, int pageSize);
        Task<Product?> GetProductByIdAsync(int id);
    }

}