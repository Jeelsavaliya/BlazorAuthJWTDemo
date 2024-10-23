using BlazorAuthClientApp.Model;
using BlazorAuthClientApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAuthClientApp.Pages
{
    public partial class Register
    {
        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private RegisterModel user = new RegisterModel();

        private async Task RegisterSuccess()
        {
            await authService.Register(user);
            navigationManager.NavigateTo("/login");
        }
    }
}
