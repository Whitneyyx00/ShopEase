using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string searchTerm = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<Product> GetProductAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
    }
}
