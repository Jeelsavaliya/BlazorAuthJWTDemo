namespace BlazorAuthAPI.DTO
{
    public class UserSessionDto
    {
        public string? Id { get; }
        public string? Name { get; }
        public string? Email { get; }
        public string? Role { get; }

        public UserSessionDto(string? id, string? name, string? email, string? role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
