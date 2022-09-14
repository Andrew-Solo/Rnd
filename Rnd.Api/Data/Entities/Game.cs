using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index( nameof(OwnerId), nameof(Name), IsUnique = true)]
public class Game : IEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset? Edited { get; set; }

    #region Navigation

    public virtual List<Member> Members { get; set; } = new();

    #endregion
}