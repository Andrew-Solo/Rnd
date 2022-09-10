using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Member), nameof(Name), IsUnique = true)]
public class Character
{
    public Guid Id { get; set; }
    
    public virtual Member Member { get; set; } = null!;
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;
    
    public bool Locked { get; set; } = false;
    
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public virtual List<Field> Fields { get; set; } = new();
    public virtual List<Parameter> Parameters { get; set; } = new();
    public virtual List<Resource> Resources { get; set; } = new();
    public virtual List<Effect> Effects { get; set; } = new();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? Edited { get; set; }
    public DateTime? LastPick { get; set; }
}