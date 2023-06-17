using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Primitives;
using Rnd.Results;

namespace Rnd.Models;

[Index(nameof(Name), IsUnique = true)]
public class User : Model
{
    [MaxLength(TextSize.Hash)]
    public string PasswordHash { get; protected set; }

    [MaxLength(TextSize.Tiny)]
    public Role Role { get; protected set; } = Role.Viewer;

    [Column(TypeName = "json")]
    public List<Association> Associations { get; protected set; } = new();

    public virtual List<Member> Memberships { get; } = new();

    protected User(string path, string name, string passwordHash) : base(path, name)
    {
        PasswordHash = passwordHash;
    }

    public static Result<User> Create(UserData data)
    {
        Guard.Against.Null(data.Path);
        Guard.Against.Null(data.Name);
        Guard.Against.Null(data.Password);
        
        var user = new User(data.Path, data.Name, data.Password);
        
        user.FillModel(data);
        
        return Result.Ok(user);
    }
    
    protected override void FillModel(ModelData data)
    {
        base.FillModel(data);
        var userData = (UserData) data;
        if (userData.Role != null) Role = userData.Role.Value;
        if (userData.Associations != null) Associations = userData.Associations;
    }
}
