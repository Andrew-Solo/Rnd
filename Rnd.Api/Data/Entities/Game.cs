namespace Rnd.Api.Data.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<Member> Members { get; set; } = new();
}