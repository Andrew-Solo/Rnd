using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
public class Character
{
    #region Factories

    protected Character() { }

    #endregion
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    
    public virtual Member Owner { get; protected set; } = null!;
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public virtual CharacterInstance Instance { get; protected set; } = null!;

    public DateTimeOffset Created { get; protected set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset? Selected { get; set; }

    #region Navigation
    
    public Guid OwnerId { get; protected set; }

    #endregion
}