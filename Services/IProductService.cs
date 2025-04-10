// Services/IProductService.cs
using ShopEase.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(string? searchTerm = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<Product> GetProductAsync(int id);
    }
}