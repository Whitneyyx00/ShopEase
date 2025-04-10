// Services/ProductService.cs
using ShopEase.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace ShopEase.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiBaseUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<Product>> GetProductsAsync(string? searchTerm = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                return new List<Product>();
            }

            var uriBuilder = new UriBuilder($"{_apiBaseUrl}/api/Products");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query["searchTerm"] = searchTerm;
            }
            if (minPrice.HasValue)
            {
                query["minPrice"] = minPrice.ToString();
            }
            if (maxPrice.HasValue)
            {
                query["maxPrice"] = maxPrice.ToString();
            }

            uriBuilder.Query = query.ToString();
            string url = uriBuilder.ToString();

            return await _httpClient.GetFromJsonAsync<List<Product>>(url) ?? new List<Product>();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                throw new InvalidOperationException("ApiBaseUrl is not configured.");
            }

            var product = await _httpClient.GetFromJsonAsync<Product>($"{_apiBaseUrl}/api/Products/{id}");
            return product ?? throw new InvalidOperationException($"Product with ID {id} was not found.");
        }
    }
}