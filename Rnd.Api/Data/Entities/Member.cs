using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Helpers;

namespace Rnd.Api.Data.Entities;

[Index(nameof(GameId), nameof(UserId), IsUnique = true)]
public class Member : IEntity
{
    public static Member Create(Guid gameId, Guid userId, string nickname)
    {
        return new Member
        {
            Id = Guid.NewGuid(),
            GameId = gameId,
            UserId = userId,
            Role = MemberRole.Player,
            Nickname = nickname,
            ColorHex = ColorTranslator.ToHtml(ColorHelper.PickRandomDefault()),
        };
    }
    
    public static Member Create(Guid gameId, MemberFormModel form)
    {
        var member = Create(gameId, form.UserId!.Value, form.Nickname!);

        if (form.Role != null) member.Role = EnumHelper.Parse<MemberRole>(form.Role);
        if (form.ColorHex != null) member.ColorHex = form.ColorHex;

        return member;
    }
    
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    
    [MaxLength(32)]
    public MemberRole Role { get; set; }

    [MaxLength(50)]
    public string Nickname { get; set; } = null!;
    
    [MaxLength(32)]
    public string ColorHex { get; set; } = null!;

    //TODO вообще а зачем эта штука я забыл
    public DateTimeOffset LastActivity { get; set; }

    #region Navigation

    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual List<Character> Characters { get; set; } = new();

    #endregion
}