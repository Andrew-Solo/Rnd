using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Data.Entities;

[Index( nameof(FounderId), nameof(Name), IsUnique = true)]
public class Game : IEntity
{
    public static Game Create(Guid founderId, string name)
    {
        var game = new Game
        {
            FounderId = founderId,
            Name = name,
        };

        return game;
    }

    public static Game Create(Guid founderId, GameFormModel form)
    {
        var game = Create(founderId, form.Name!);

        if (form.Title != null) game.Title = form.Title;
        if (form.Description != null) game.Description = form.Description;

        return game;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    
    public virtual User Founder { get; set; } = null!;

    [MaxLength(32)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public virtual Module? Module { get; set; }
    
    public virtual List<Member> Members { get; set; } = new();

    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset? Edited { get; set; }
    //TODO сделать логику выбора текущей и сортировки по последним
    //public DateTimeOffset? Selected { get; set; }

    #region Navigation

    public Guid FounderId { get; set; }
    
    public Guid? ModuleId { get; set; }

    #endregion
}