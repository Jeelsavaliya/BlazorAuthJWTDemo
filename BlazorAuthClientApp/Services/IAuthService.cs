using BlazorAuthClientApp.Model;

namespace BlazorAuthClientApp.Services
{
    public interface IAuthService
    {
        Task<string> Login(User user);
        Task<string> Register(User user);
        Task<string> Logout();


    }
}
