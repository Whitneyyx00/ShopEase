// Services/AccountService.cs
using ShopEase.Client.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace ShopEase.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
private readonly IJSRuntime _jsRuntime;

        public AccountService(HttpClient httpClient, IConfiguration configuration, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? throw new ArgumentNullException("ApiBaseUrl", "ApiBaseUrl configuration is missing.");
_jsRuntime = jsRuntime;
        }

        public async Task RegisterUser(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/Account/Register", registerModel);
            response.EnsureSuccessStatusCode();
        }

        public async Task LoginUser(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/Account/Login", loginModel);
            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadAsStringAsync();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        }

        public async Task LogoutUser()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }
}