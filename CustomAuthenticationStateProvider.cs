// CustomAuthenticationStateProvider.cs
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components; // Add this line

namespace ShopEase.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAccessTokenProvider _tokenProvider;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;

        public CustomAuthenticationStateProvider(IAccessTokenProvider tokenProvider,
            NavigationManager navigationManager,
            ILogger<CustomAuthenticationStateProvider> logger)
        {
            _tokenProvider = tokenProvider;
            _navigationManager = navigationManager;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity());

                var tokenResult = await _tokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "ShopEase User") }, "ShopEaseAuth"));
                    return new AuthenticationState(user);
                }

                return new AuthenticationState(user);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}