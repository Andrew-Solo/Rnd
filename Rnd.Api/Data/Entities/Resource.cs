namespace Rnd.Api.Data.Entities;

public class Resource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Value { get; set; }
    public int Default { get; set; } = 0;
    public int? Min { get; set; }
    public int? Max { get; set; }
}