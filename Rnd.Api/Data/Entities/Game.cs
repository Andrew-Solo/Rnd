using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Data.Entities;

[Index( nameof(OwnerId), nameof(Name), IsUnique = true)]
public class Game : IEntity
{
    public static Game Create(Guid ownerId, string name)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Name = name,
            Created = DateTimeOffset.Now.UtcDateTime
        };

        return game;
    }

    public static Game Create(Guid ownerId, GameFormModel form)
    {
        var game = Create(ownerId, form.Name!);
        
        game.Title = form.Title;
        game.Description = form.Description;

        return game;
    }
    
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    
    [MaxLength(32)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Edited { get; set; }
    //TODO сделать логику сортировки по просмотрам
    //public DateTimeOffset? Viewed { get; set; }

    #region Navigation

    public virtual List<Member> Members { get; set; } = new();

    #endregion
}