using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Modules.Basic.Parameters;

public interface IParameter : IStorable<Parameter>
{
    public Guid CharacterId { get; }
    public string? Path { get; }
    public string Name { get; }
    public Type Type { get; }
    public object? Value { get; set; }
}