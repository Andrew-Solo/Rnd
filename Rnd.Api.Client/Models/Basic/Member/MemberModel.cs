namespace Rnd.Api.Client.Models.Basic.Member;

public class MemberModel
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string ColorHtml { get; set; } = null!;
    public DateTimeOffset Created { get; set; }
    
    public MemberModel Clone()
    {
        return new MemberModel
        {
            Id = Id,
            GameId = GameId,
            UserId = UserId,
            Role = Role,
            Nickname = Nickname,
            ColorHtml = ColorHtml,
            Created = Created,
        };
    }

    public MemberFormModel ToForm()
    {
        return new MemberFormModel
        {
            Nickname = Nickname,
            Role = Role,
            ColorHtml = ColorHtml,
            UserId = UserId,
        };
    }
}