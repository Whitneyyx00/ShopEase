// Services/IOrderService.cs
using ShopEase.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(int orderId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}