using BlazorAuthClientApp.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorAuthClientApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Login(LoginModel user)
        {
            var response = await _httpClient.PostAsJsonAsync("AuthAPI/login", user);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<TokenResponse>(token).Token;
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", apiResult);

                //Type Cast for access to the base class and need to invoke specific methods on a derived class.
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(apiResult);
                
            }
            else
            {
                throw new Exception("Login failed.");
            }
        }

        public async Task Register(RegisterModel user)
        {
            var response = await _httpClient.PostAsJsonAsync("AuthAPI/register", user);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Registration failed.");
            }
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogout();
        }
    }
}
