namespace Rnd.Api.Data.Entities;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = nameof(String);
    public string ValueJson { get; set; } = null!;
}