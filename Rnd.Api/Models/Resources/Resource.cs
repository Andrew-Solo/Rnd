namespace Rnd.Api.Models.Resources;

public class Resource : IResource
{
    public Resource(Guid id, string group, string name)
    {
        Id = id;
        Path = group;
        Name = name;
    }

    public Guid Id { get; }
    public string? Path { get; }
    public string Name { get; }
    
    public decimal Default => 0;
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}