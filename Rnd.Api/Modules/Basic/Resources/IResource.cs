using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Resources;

public interface IResource : IStorable<Data.Entities.Resource>
{
    public Guid CharacterId { get; }
    public string? Path { get; }
    public string Name { get; }
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}