using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDTO>> GetPagedProductsAsync(int? categoryId, int? page, int? pageSize);
        Task<ProductResponseDTO?> GetProductByIdAsync(int id);
    }

}