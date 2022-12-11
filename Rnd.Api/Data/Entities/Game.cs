using System.ComponentModel.DataAnnotations;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Data.Entities;

public class Game
{
    #region Factories

    protected Game() { }
    
    public static Game Create(User owner, string name)
    {
        var game = new Game
        {
            Name = name,
        };
        
        game.Members.Add(Member.Create(game.Id, new MemberFormModel
        {
            UserId = owner.Id,
            Nickname = owner.Login,
            Role = MemberRole.Owner.ToString(),
        }));
        
        return game;
    }

    public static Game Create(User owner, GameFormModel form)
    {
        var game = Create(owner, form.Name ?? throw new InvalidOperationException());

        game.Title = form.Title;
        game.Description = form.Description;

        return game;
    }

    #endregion

    public Guid Id { get; protected set; } = Guid.NewGuid();

    [MaxLength(32)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public virtual Module? Module { get; set; }
    
    public virtual List<Member> Members { get; protected set; } = new();

    public DateTimeOffset Created { get; protected set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset Selected { get; protected set; } = DateTimeOffset.Now.UtcDateTime;

    #region Navigation
    
    public Guid? ModuleId { get; protected set; }

    #endregion

    public void SetForm(GameFormModel form)
    {
        if (form.Name != null) Name = form.Name;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        if (form.ModuleId != null) ModuleId = form.ModuleId;
    }

    public void Select()
    {
        Selected = DateTimeOffset.Now.UtcDateTime;
    }
}