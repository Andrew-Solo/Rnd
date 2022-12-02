using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(GameId), nameof(UserId), IsUnique = true)]
public class Member : IEntity
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    
    [MaxLength(32)]
    public MemberRole Role { get; set; }

    [MaxLength(50)]
    public string Nickname { get; set; } = null!;
    
    [MaxLength(32)]
    public string ColorHex { get; set; } = null!;

    public DateTimeOffset LastActivity { get; set; }

    #region Navigation

    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual List<Character> Characters { get; set; } = new();

    #endregion
}