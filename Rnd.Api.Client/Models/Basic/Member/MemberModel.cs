namespace Rnd.Api.Client.Models.Basic.Member;

public class MemberModel
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public DateTimeOffset LastActivity { get; set; }
    
    public MemberModel Clone()
    {
        return new MemberModel
        {
            Id = Id,
            GameId = GameId,
            UserId = UserId,
            Role = Role,
            Nickname = Nickname,
            ColorHex = ColorHex,
            LastActivity = LastActivity,
        };
    }

    public MemberFormModel ToForm()
    {
        return new MemberFormModel
        {
            Nickname = Nickname,
            Role = Role,
            ColorHex = ColorHex,
            UserId = UserId,
        };
    }
}