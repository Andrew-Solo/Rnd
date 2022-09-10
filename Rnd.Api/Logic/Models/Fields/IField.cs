using Microsoft.VisualBasic.FileIO;

namespace Rnd.Api.Logic.Models.Fields;

public interface IField
{
    public Guid Id { get; }
    public string? Path { get; }
    public string Name { get; }
    public FieldType Type { get; }
    public object? Value { get; set; }
}