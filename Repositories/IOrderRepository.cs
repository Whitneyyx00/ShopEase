using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(string userId);
        Task<Order> GetOrderAsync(int id, string userId);
        Task<Order> CreateOrderAsync(Order order);
    }
}
