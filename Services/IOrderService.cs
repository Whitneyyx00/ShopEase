using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(string userId);
        Task<Order> GetOrderAsync(int id, string userId);
        Task<Order> CreateOrderAsync(string userId, string shippingAddress, string paymentMethod);
    }
}
