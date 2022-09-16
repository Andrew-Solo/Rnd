namespace Rnd.Api.Models.User;

public class UserModel
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTimeOffset RegistrationDate { get; set; }
}