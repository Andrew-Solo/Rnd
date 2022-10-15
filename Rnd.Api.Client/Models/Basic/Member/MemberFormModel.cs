namespace Rnd.Api.Client.Models.Basic.Member;

public class MemberFormModel
{
    public Guid? UserId { get; set; }
    public string? Role { get; set; }
    public string? Nickname { get; set; }
    public string? ColorHex { get; set; }
}