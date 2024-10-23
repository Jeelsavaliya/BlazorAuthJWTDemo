using BlazorAuthClientApp.Model;
using BlazorAuthClientApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace BlazorAuthClientApp.Pages
{
    public partial class Login
    {
        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        public AuthService authService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

       

        private LoginModel user = new LoginModel();

        private string errorMessage;

        private bool isAuthenticated;

        protected override async Task OnInitializedAsync()
        {
            //If user is authorize and back to login page, login page not to be show
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            isAuthenticated = authState.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                navigationManager.NavigateTo("/home");
            }
        }


        private async Task LoginSuccess()
        {
            try
            {
                await authService.Login(user);
                navigationManager.NavigateTo("/home");
            }
            catch (Exception)
            {
                errorMessage = "Login failed";
            }
        }
    }
}
