using System.Linq;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepository;
        private readonly IRepository<int, Category> _categoryRepository;
        private readonly IRepository<int, Color> _colorRepository;
        private readonly IRepository<int, Model> _modelRepository;

        public ProductService(IRepository<int, Product> productRepository, IRepository<int, Category> categoryRepository, IRepository<int, Color> colorRepository, IRepository<int, Model> modelRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetPagedProductsAsync(int? categoryId, int? page, int? pageSize)
        {
            var allProducts = await _productRepository.GetAll();
            var productsList = new List<ProductResponseDTO>();
            if (categoryId.HasValue)
            {
                allProducts = allProducts.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            if (allProducts == null || !allProducts.Any())
                return Enumerable.Empty<ProductResponseDTO>();

            foreach (var product in allProducts)
            {
                var productDto = await MapProductToDto(product);
                if (productDto != null)
                {
                    productsList.Add(productDto);
                }
            }

            if (productsList.Count == 0)
            {
                return Enumerable.Empty<ProductResponseDTO>();
            }

            if (page == null || pageSize == null || page <= 0 || pageSize <= 0)
            {
                return productsList;
            }
            int actualPage = page ?? 1;
            int actualPageSize = pageSize ?? 10;

            return productsList
                .OrderByDescending(p => p.ProductId)
                .Skip((actualPage - 1) * actualPageSize)
                .Take(actualPageSize);
        }


        public async Task<ProductResponseDTO?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.Get(id);
            if (product == null)
                return null;

            return await MapProductToDto(product);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsAsync(int? categoryId, int page, int pageSize)
        {
            return await GetPagedProductsAsync(categoryId, page, pageSize);
        }

        private async Task<ProductResponseDTO> MapProductToDto(Product product)
        {
            var category = await _categoryRepository.Get(product.CategoryId ?? 0);
            var color = await _colorRepository.Get(product.ColorId ?? 0);
            var model = await _modelRepository.Get(product.ModelId ?? 0);

            return new ProductResponseDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Image = product.Image,
                Price = product.Price,
                Category = category?.Name ?? string.Empty,
                Color = color?.Color1 ?? string.Empty,
                Model = model?.Model1 ?? string.Empty,
                Storage = "128GB",
                SellStartDate = product.SellStartDate ?? DateTime.MinValue,
                SellEndDate = product.SellEndDate ?? DateTime.MinValue
            };
        }
    }

}
