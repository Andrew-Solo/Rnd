namespace Rnd.Api.Models.User;

public class UserRegisterModel
{
    public string? Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}