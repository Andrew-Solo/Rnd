namespace Rnd.Api.Data.Entities;

public class Domain
{
    public Guid Id { get; set; }
    public DomainType Type { get; set; }
    public int Value { get; set; }
}