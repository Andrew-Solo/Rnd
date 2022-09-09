namespace Rnd.Api.Data.Entities;

public class Field
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public FieldType Type { get; set; }
    public string ValueJson { get; set; } = null!;
}