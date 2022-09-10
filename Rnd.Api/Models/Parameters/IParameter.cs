namespace Rnd.Api.Models.Parameters;

public interface IParameter
{
    public Guid Id { get; }
    public string? Path { get; }
    public string Name { get; }
    public Type Type { get; }
    public object? Value { get; set; }
}