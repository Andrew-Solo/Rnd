using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
public class Character : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public virtual Member Owner { get; set; } = null!;
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public virtual CharacterInstance Instance { get; set; } = null!;

    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset? Edited { get; set; }
    public DateTimeOffset? LastPick { get; set; }

    #region Navigation
    
    public Guid OwnerId { get; set; }

    #endregion
}