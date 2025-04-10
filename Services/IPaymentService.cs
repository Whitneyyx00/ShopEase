// Services/IPaymentService.cs
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntent(decimal amount, string currency);
    }
}