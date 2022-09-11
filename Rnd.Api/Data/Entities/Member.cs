using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Modules.Basic.Members;

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
    
    /// <summary>
    /// Format: #001122AA
    /// </summary>
    [MaxLength(9)]
    public string ColorHex { get; set; } = null!;

    public DateTime LastActivity { get; set; }

    #region Navigation

    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual List<Character> Characters { get; set; } = new();

    #endregion
}