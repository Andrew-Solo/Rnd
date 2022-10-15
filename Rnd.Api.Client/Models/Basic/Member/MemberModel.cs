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
}