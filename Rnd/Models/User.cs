using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Constants;
using Rnd.Results;

namespace Rnd.Models;

public class User : Model
{
    [MaxLength(TextSize.Hash)]
    public string PasswordHash { get; protected set; }

    [MaxLength(TextSize.Tiny)]
    public Role Role { get; protected set; } = Role.Viewer;

    [Column(TypeName = "json")]
    public List<Association> Associations { get; protected set; } = new();

    public virtual List<Member> Memberships { get; protected set; } = new();

    protected User(string name, string path, string passwordHash) : base(name, path)
    {
        PasswordHash = passwordHash;
    }

    public static Result<User> Create(dynamic data)
    {
        var user = new User(data.Name, data.Path, data.PasswordHash);
        return Result.Ok(user);
    }
}

public enum Role : byte
{
    Viewer,
    Editor,
    Moderator,
    Admin,
    Owner
}

public record struct Association(
    Provider Provider,
    string Identifier,
    string? Secret = null
);

public enum Provider : byte
{
    Discord,
}