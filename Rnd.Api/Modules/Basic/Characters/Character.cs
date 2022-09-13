using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Modules.Basic.Effects;
using Rnd.Api.Modules.Basic.Fields;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Effect = Rnd.Api.Data.Entities.Effect;
using Resource = Rnd.Api.Data.Entities.Resource;

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
        entity.Fields.SaveList(Fields.Cast<IStorable<Field>>().ToList());
        entity.Parameters.SaveList(Parameters.Cast<IStorable<Parameter>>().ToList());
        entity.Resources.SaveList(Resources.Cast<IStorable<Resource>>().ToList());
        entity.Effects.SaveList(Effects.Cast<IStorable<Effect>>().ToList());
        entity.Created = Created;
        entity.Edited = Edited;
        entity.LastPick = LastPick;

        return entity;
    }

    public IStorable<Data.Entities.Character>? Load(Data.Entities.Character entity)
    {
        if (AsStorable.NotLoad(entity)) return null;

        OwnerId = entity.MemberId;
        Name = entity.Name;
        Locked = entity.Locked;
        Title = entity.Title;
        Description = entity.Description;
        Fields.Cast<IStorable<Field>>().ToList().LoadList(entity.Fields, new FieldFactory());
        Parameters.Cast<IStorable<Parameter>>().ToList().LoadList(entity.Parameters, new ParameterFactory());
        Resources.Cast<IStorable<Resource>>().ToList().LoadList(entity.Resources, new ResourceFactory());
        Effects.Cast<IStorable<Effect>>().ToList().LoadList(entity.Effects, new EffectFactory());
        Created = entity.Created;
        Edited = entity.Edited;
        LastPick = entity.LastPick;

        return AsStorable;
    }

    #endregion
}