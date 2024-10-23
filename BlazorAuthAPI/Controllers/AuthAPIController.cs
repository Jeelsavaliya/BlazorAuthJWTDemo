using BlazorClass.Contracts;
using BlazorClass.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BlazorClass.DTO.ServiceResponseDto;

namespace BlazorAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IUserAccount _userAccount;
        public AuthAPIController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpPost("register")]
        public async Task<GeneralResponse> CreateAccount(UserDto userDto)
        {
            var response = await _userAccount.CreateAccount(userDto);
            return response;
        }

        [HttpPost("login")]
        public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
        {
            var response = await _userAccount.LoginAccount(loginDto);
            return response;
        }
    }
}
