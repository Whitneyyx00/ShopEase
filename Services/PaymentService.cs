// Services/PaymentService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ShopEase.Client.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? throw new ArgumentNullException(nameof(configuration), "ApiBaseUrl cannot be null.");
        }

        public async Task<string> CreatePaymentIntent(decimal amount, string currency)
        {
            var paymentIntentRequest = new
            {
                Amount = amount,
                Currency = currency
            };

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/Payment/CreatePaymentIntent", paymentIntentRequest);
            response.EnsureSuccessStatusCode();

            var paymentIntentId = await response.Content.ReadAsStringAsync();
            return paymentIntentId;
        }
    }
}