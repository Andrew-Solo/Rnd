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

    #region Equals

    protected bool Equals(GameModel other)
    {
        return Id.Equals(other.Id) &&
               OwnerId.Equals(other.OwnerId) &&
               Name == other.Name &&
               Title == other.Title &&
               Description == other.Description &&
               Created.Equals(other.Created) &&
               Nullable.Equals(Edited, other.Edited);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GameModel) obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(Id, OwnerId, Name, Title, Description, Created, Edited);
    }

    public GameModel Clone()
    {
        return new GameModel
        {
            Id = Id,
            OwnerId = OwnerId,
            Name = Name,
            Title = Title,
            Description = Description,
            Created = Created,
            Edited = Edited,
        };
    }

    #endregion
}