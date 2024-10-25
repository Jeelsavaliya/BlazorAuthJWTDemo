using BlazorAuthAPI.DTO;
using static BlazorAuthAPI.DTO.ServiceResponseDto;

namespace BlazorAuthAPI.Services
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDto userDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);
    }
}
