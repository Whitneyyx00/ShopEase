// Services/IAccountService.cs
using ShopEase.Client.Models;
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterModel registerModel);
        Task LoginUser(LoginModel loginModel);
        Task LogoutUser();
    }
}