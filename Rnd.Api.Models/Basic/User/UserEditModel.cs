namespace Rnd.Api.Models.Basic.User;

public class UserEditModel
{
    public Guid Id { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}