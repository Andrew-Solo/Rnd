using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(MemberId), nameof(Name), IsUnique = true)]
public class Character : IEntity
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }

    [MaxLength(32)]
    public string Module { get; set; } = null!;
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    public bool Locked { get; set; }
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset? Edited { get; set; }
    public DateTimeOffset? LastPick { get; set; }

    #region Navigation

    public virtual Member Member { get; set; } = null!;
    public virtual List<Field> Fields { get; set; } = new();
    public virtual List<Parameter> Parameters { get; set; } = new();
    public virtual List<Resource> Resources { get; set; } = new();
    public virtual List<Effect> Effects { get; set; } = new();

    #endregion
}