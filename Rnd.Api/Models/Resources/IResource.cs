namespace Rnd.Api.Models.Resources;

public interface IResource
{
    public string Group { get; }
    public string Name { get; }
    public decimal Default { get; }
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}