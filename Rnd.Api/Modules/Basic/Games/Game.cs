using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Members;

namespace Rnd.Api.Modules.Basic.Games;

public class Game : IStorable<Data.Entities.Game>
{
    public Game(IEntity entity)
    {
        Id = entity.Id;
        
        Name = null!;
        
        Members = new List<Member>();
    }
    
    public Game(Guid ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.Now.UtcDateTime;
        Members = new List<Member>();
    }

    public Guid Id { get; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Member> Members { get; private set; }
    
    public DateTimeOffset Created { get; private set; }
    public DateTimeOffset? Edited { get; set; }
    
    #region IStorable

    public IStorable<Data.Entities.Game> AsStorable => this;

    public Data.Entities.Game? Save(Data.Entities.Game? entity, Action<IEntity>? setAddedState = null, bool upcome = true)
    {
        entity ??= new Data.Entities.Game {Id = Id};
        if (AsStorable.NotSave(entity)) return null;

        entity.OwnerId = OwnerId;
        entity.Name = Name;
        entity.Title = Title;
        entity.Description = Description;
        if (upcome) entity.Members.SaveList(Members.Cast<IStorable<Data.Entities.Member>>().ToList(), setAddedState);
        entity.Created = Created;
        entity.Edited = Edited;

        return entity;
    }

    public IStorable<Data.Entities.Game>? Load(Data.Entities.Game entity, bool upcome = true)
    {
        if (AsStorable.NotLoad(entity)) return null;

        OwnerId = entity.OwnerId;
        Name = entity.Name;
        Title = entity.Title;
        Description = entity.Description;
        
        if (upcome) Members = Members
            .Cast<IStorable<Data.Entities.Member>>().ToList()
            .LoadList(entity.Members, new MemberFactory())
            .Cast<Member>().ToList();
        
        Created = entity.Created;
        Edited = entity.Edited;
        
        return AsStorable;
    }

    #endregion
}