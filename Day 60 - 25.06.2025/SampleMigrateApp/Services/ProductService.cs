using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepository;

        public ProductService(IRepository<int, Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int? categoryId, int page, int pageSize)
        {
            var allProducts = await _productRepository.GetAll();

            var query = allProducts
                .OrderByDescending(p => p.ProductId)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int? categoryId, int page, int pageSize)
        {
            return await GetPagedProductsAsync(categoryId, page, pageSize);
        }
    }

}
