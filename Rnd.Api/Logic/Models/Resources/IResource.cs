using Rnd.Api.Data;

namespace Rnd.Api.Logic.Models.Resources;

public interface IResource : IStorable<Data.Entities.Resource>
{
    public string? Path { get; }
    public string Name { get; }
    public decimal Default { get; }
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}