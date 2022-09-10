using System.ComponentModel.DataAnnotations;

namespace Rnd.Api.Data.Entities;

public class Resource : IEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Fullname { get; set; } = null!;
    
    public decimal Value { get; set; }
    
    public decimal Default { get; set; } = 0;
    
    public decimal? Min { get; set; }
    
    public decimal? Max { get; set; }
}