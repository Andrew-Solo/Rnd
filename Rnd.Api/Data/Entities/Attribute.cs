namespace Rnd.Api.Data.Entities;

public class Attribute
{
    public Guid Id { get; set; }
    public AttributeType Type { get; set; }
    public int Value { get; set; }
}