namespace Rnd.Api.Models.Resources;

public class Resource : IResource
{
    public Resource(string group, string name)
    {
        Group = group;
        Name = name;
    }

    public string Group { get; }
    public string Name { get; }
    public decimal Default => 0;
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}