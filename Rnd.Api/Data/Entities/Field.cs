using System.ComponentModel.DataAnnotations;
using Rnd.Api.Models.Fields;

namespace Rnd.Api.Data.Entities;

public class Field
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = null!;
    
    [MaxLength(32)]
    public FieldType Type { get; set; }
    
    public string ValueJson { get; set; } = null!;
}