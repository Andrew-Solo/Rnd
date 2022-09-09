using System.ComponentModel.DataAnnotations;

namespace Rnd.Api.Data.Entities;

public class Parameter
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = null!;
    
    [MaxLength(511)]
    public string Type { get; set; } = nameof(Int32);
    
    public string ValueJson { get; set; } = null!;
}