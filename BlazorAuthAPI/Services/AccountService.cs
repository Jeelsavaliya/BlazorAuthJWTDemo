using BlazorAPI.Data;
using BlazorAuthAPI.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BlazorAuthAPI.DTO.ServiceResponseDto;

namespace BlazorAuthAPI.Services
{
    public class AccountService : IUserAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<GeneralResponse> CreateAccount(UserDto userDto)
        {
            if (userDto == null)
            {
                return new GeneralResponse(false, "Model is empty");
            }

            var newUser = new ApplicationUser()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                UserName = userDto.Email
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new GeneralResponse(false, "User Register Already");
            }

            var createUser = await _userManager.CreateAsync(newUser!, userDto.Password);
            if (!createUser.Succeeded)
            {
                return new GeneralResponse(false, "Error occured...Please try again");
            }

            //Assign Role: Default first registr Admin, rest User
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");

            if (checkAdmin == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin"});
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkRole = await _roleManager.FindByIdAsync("User");

                if(checkRole == null)
                    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });

                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
        {
            if(loginDto == null)
            {
                return new LoginResponse(false, null!, "Login container is null");
            }

            var getUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (getUser == null)
            {
                return new LoginResponse(false, null!, "User not found");
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(getUser, loginDto.Password);
            if (!checkPassword)
            {
                return new LoginResponse(false, null!, "Invalid email/Password");
            }

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSessionDto(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());

            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSessionDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
