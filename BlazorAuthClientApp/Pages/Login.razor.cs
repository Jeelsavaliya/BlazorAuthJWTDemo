using BlazorAuthClientApp.Model;
using BlazorAuthClientApp.Services;
using Microsoft.AspNetCore.Components;


namespace BlazorAuthClientApp.Pages
{
    public partial class Login
    {
        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private LoginModel user = new LoginModel();

        
        private async Task LoginSuccess()
        {
            await authService.Login(user);
            navigationManager.NavigateTo("/home");
        }

        
        private void GoToRegister()
        {
            navigationManager.NavigateTo("/register");
        }
    }
}
