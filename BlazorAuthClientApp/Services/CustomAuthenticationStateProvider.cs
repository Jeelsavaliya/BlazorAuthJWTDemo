using BlazorAuthClientApp.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorAuthClientApp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
            
            ClaimsPrincipal user;

            if (string.IsNullOrEmpty(token))
            {
                user = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token);

                var claims = jwtToken.ToString();
                var identity = new ClaimsIdentity("jwt");
                user = new ClaimsPrincipal(identity);
            }

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            ClaimsPrincipal authenticatedUser;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token);

            var claims = jwtToken.ToString();
            var identity = new ClaimsIdentity("jwt");
            authenticatedUser = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }

}
