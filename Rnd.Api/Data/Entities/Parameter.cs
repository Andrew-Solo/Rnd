using System.ComponentModel.DataAnnotations;

namespace Rnd.Api.Data.Entities;

public class Parameter : IEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Fullname { get; set; } = null!;
    
    [MaxLength(511)]
    public string Type { get; set; } = nameof(Int32);
    
    public string ValueJson { get; set; } = null!;
}