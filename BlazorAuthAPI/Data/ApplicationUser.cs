
using Microsoft.AspNetCore.Identity;

namespace BlazorAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}

