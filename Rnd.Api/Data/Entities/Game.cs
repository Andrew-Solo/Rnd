using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Game
{
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public DateTime? Edited { get; set; }
    
    public virtual List<Member> Members { get; set; } = new();
}