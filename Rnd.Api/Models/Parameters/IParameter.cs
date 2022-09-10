namespace Rnd.Api.Models.Parameters;

public interface IParameter
{
    public string Group { get; }
    public string Name { get; }
    public Type Type { get; }
    public object? Value { get; set; }
}