// Services/OrderService.cs
using ShopEase.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ShopEase.Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiBaseUrl;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                return new List<Order>();
            }
            return await _httpClient.GetFromJsonAsync<List<Order>>($"{_apiBaseUrl}/api/Order") ?? new List<Order>();
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                throw new InvalidOperationException("ApiBaseUrl is not configured.");
            }
            var order = await _httpClient.GetFromJsonAsync<Order>($"{_apiBaseUrl}/api/Order/{orderId}");
            if (order == null)
            {
                throw new InvalidOperationException("Order not found or response was null.");
            }
            return order;
        }

        public async Task CreateOrderAsync(Order order)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                return;
            }
            await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/Order", order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                return;
            }

            var json = JsonSerializer.Serialize(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/Order/{order.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                Console.WriteLine("ApiBaseUrl is not configured.");
                return;
            }

            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/Order/{orderId}");
            response.EnsureSuccessStatusCode();
        }
    }
}