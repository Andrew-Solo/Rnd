namespace RnDApi.Data.Entities;

public class Member
{
    public Guid Id { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Game Game { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public Role Role { get; set; }
}