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
            GameId = gameId,
            UserId = userId,
            Nickname = nickname,
        };
    }
    
    public static Member Create(Guid gameId, MemberFormModel form)
    {
        var member = Create(gameId, form.UserId!.Value, form.Nickname!);

        if (form.Role != null) member.Role = EnumHelper.Parse<MemberRole>(form.Role);
        if (form.ColorHex != null) member.Color = ColorTranslator.FromHtml(form.ColorHex);

        return member;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public virtual Game Game { get; set; } = null!;
    
    public virtual User User { get; set; } = null!;

    [MaxLength(32)] 
    public MemberRole Role { get; set; } = MemberRole.Player;

    [MaxLength(50)]
    public string Nickname { get; set; } = null!;
    
    public Color Color { get; set; } = ColorHelper.PickRandomDefault();
    
    public virtual List<Character> Characters { get; set; } = new();

    //TODO вообще а зачем эта штука я забыл
    public DateTimeOffset LastActivity { get; set; }

    #region Navigation

    public Guid GameId { get; set; }
    public Guid UserId { get; set; }

    #endregion
}