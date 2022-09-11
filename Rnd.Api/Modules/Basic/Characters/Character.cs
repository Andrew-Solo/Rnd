using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Effects;
using Rnd.Api.Modules.Basic.Fields;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.Basic.Characters;

public class Character : ICharacter
{
    public Character(Guid ownerId, string name)
    {
        OwnerId = ownerId;
        Name = name;

        Id = Guid.NewGuid();
        Fields = new List<IField>();
        Parameters = new List<IParameter>();
        Resources = new List<IResource>();
        Effects = new List<IEffect>();
        
        Created = DateTime.Now;
    }

    public Guid Id { get; }
    public Guid OwnerId { get; set; }
    public string Name { get; private set; }
    public bool Locked { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<IField> Fields { get; }
    public List<IParameter> Parameters { get; }
    public List<IResource> Resources { get; }
    public List<IEffect> Effects { get; }
    
    public DateTime Created { get; private set; }
    public DateTime? Edited { get; set; }
    public DateTime? LastPick { get; set; }

    #region IStorable
    
    public void Save(Data.Entities.Character entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        entity.MemberId = OwnerId;
        entity.Name = Name;
        entity.Locked = Locked;
        
        entity.Title = Title;
        entity.Description = Description;
        
        entity.Fields = Fields.Select(f => f.CreateEntity()).ToList();
        entity.Parameters = Parameters.Select(p => p.CreateEntity()).ToList();
        entity.Resources = Resources.Select(r => r.CreateEntity()).ToList();
        entity.Effects = Effects.Select(e => e.CreateEntity()).ToList();

        entity.Created = Created;
        entity.Edited = Edited;
        entity.LastPick = LastPick;
    }

    public void Load(Data.Entities.Character entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        OwnerId = entity.MemberId;
        Name = entity.Name;
        Locked = entity.Locked;

        Title = entity.Title;
        Description = entity.Description;

        Fields.Clear();
        Fields.AddRange(entity.Fields.Select(FieldFactory.ByEntity));
        
        Parameters.Clear();
        Parameters.AddRange(entity.Parameters.Select(ParameterFactory.ByEntity));
        
        Resources.Clear();
        Resources.AddRange(entity.Resources.Select(ResourceFactory.ByEntity));

        Effects.Clear();
        Effects.AddRange(entity.Effects.Select(EffectFactory.ByEntity));
        
        Created = entity.Created;
        Edited = entity.Edited;
        LastPick = entity.LastPick;
    }

    #endregion
}