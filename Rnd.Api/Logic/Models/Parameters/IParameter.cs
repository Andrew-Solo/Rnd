using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Logic.Models.Parameters;

public interface IParameter : IStorable<Parameter>
{
    public string? Path { get; }
    public string Name { get; }
    public Type Type { get; }
    public object? Value { get; set; }
}