namespace Rnd.Api.Data.Entities;

public class Member
{
    public Guid Id { get; set; }
    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public Role Role { get; set; }
    public virtual List<Character> Characters { get; set; } = new();
}