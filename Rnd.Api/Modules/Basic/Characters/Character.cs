using Rnd.Api.Data;
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
    
    public virtual List<IField> Fields { get; }
    public virtual List<IParameter> Parameters { get; }
    public virtual List<IResource> Resources { get; }
    public virtual List<IEffect> Effects { get; }
    
    public DateTime Created { get; private set; }
    public DateTime? Edited { get; set; }
    public DateTime? LastPick { get; set; }

    #region IStorable
    
    public IStorable<Data.Entities.Character> AsStorable => this;
    
    public Data.Entities.Character? Save(Data.Entities.Character? entity)
    {
        entity ??= new Data.Entities.Character {Id = Id};
        if (AsStorable.NotSave(entity)) return null;

        entity.MemberId = OwnerId;
        entity.Name = Name;
        entity.Locked = Locked;
        entity.Title = Title;
        entity.Description = Description;
        entity.Fields.SaveList(Fields.Cast<IStorable<Data.Entities.Field>>().ToList());
        entity.Parameters.SaveList(Parameters.Cast<IStorable<Data.Entities.Parameter>>().ToList());
        entity.Resources.SaveList(Resources.Cast<IStorable<Data.Entities.Resource>>().ToList());
        entity.Effects.SaveList(Effects.Cast<IStorable<Data.Entities.Effect>>().ToList());
        entity.Created = Created;
        entity.Edited = Edited;
        entity.LastPick = LastPick;

        return entity;
    }

    public void Load(Data.Entities.Character entity)
    {
        if (AsStorable.NotLoad(entity)) return;

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