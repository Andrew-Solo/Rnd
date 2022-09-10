using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Game), nameof(User), IsUnique = true)]
public class Member
{
    public Guid Id { get; set; }
    
    public virtual Game Game { get; set; } = null!;
    
    public virtual User User { get; set; } = null!;
    
    /// <summary>
    /// Format: #001122AA
    /// </summary>
    [MaxLength(9)]
    public string? ColorHex { get; set; }
    
    [MaxLength(32)]
    public MemberRole Role { get; set; }
    
    public DateTime? LastActivity { get; set; }
    
    [MaxLength(50)]
    public string Nickname { get; set; } = null!;
    
    public virtual List<Character> Characters { get; set; } = new();
}