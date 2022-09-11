using Rnd.Api.Data;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Members;

namespace Rnd.Api.Modules.Basic.Games;

public class Game : IStorable<Data.Entities.Game>
{
    public Game(Guid ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;

        Id = Guid.NewGuid();
        Created = DateTime.Now;
        Members = new List<Member>();
    }

    public Guid Id { get; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<Member> Members { get; }
    
    public DateTime Created { get; private set; }
    public DateTime? Edited { get; set; }
    
    #region IStorable

    public IStorable<Data.Entities.Game> AsStorable => this;

    public void Save(Data.Entities.Game entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        entity.OwnerId = OwnerId;
        entity.Name = Name;
        entity.Title = Title;
        entity.Description = Description;
        
        entity.Members = Members.Select(m => m.AsStorable.CreateEntity()).ToList();
        
        entity.Created = Created;
        entity.Edited = Edited;
    }

    public void Load(Data.Entities.Game entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        OwnerId = entity.OwnerId;
        Name = entity.Name;
        Title = entity.Title;
        Description = entity.Description;
        
        Members.Clear();
        Members.AddRange(entity.Members.Select(MemberFactory.ByEntity));
        
        Created = entity.Created;
        Edited = entity.Edited;
    }

    #endregion
}