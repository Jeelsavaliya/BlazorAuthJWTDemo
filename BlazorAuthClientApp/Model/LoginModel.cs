using System.ComponentModel.DataAnnotations;

namespace BlazorAuthClientApp.Model
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
