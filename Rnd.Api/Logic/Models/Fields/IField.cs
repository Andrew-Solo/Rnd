using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Logic.Models.Fields;

public interface IField : IStorable<Field>
{
    public string? Path { get; }
    public string Name { get; }
    public FieldType Type { get; }
    public object? Value { get; set; }
}