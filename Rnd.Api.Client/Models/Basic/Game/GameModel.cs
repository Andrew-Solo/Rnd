namespace Rnd.Api.Client.Models.Basic.Game;

public class GameModel
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Edited { get; set; }
}