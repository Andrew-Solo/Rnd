using System.ComponentModel.DataAnnotations;
using Rnd.Api.Models.Fields;

namespace Rnd.Api.Data.Entities;

public class Field : IEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Fullname { get; set; } = null!;
    
    [MaxLength(32)]
    public FieldType Type { get; set; }
    
    public string ValueJson { get; set; } = null!;
}