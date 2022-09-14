using Rnd.Api.Localization;

namespace Rnd.Api.Data;

// ReSharper disable once TypeParameterCanBeVariant
/// <summary>
/// The interface for synchronizing a state of this object with an entity object.
/// Used to work with a database through the entities.
/// </summary>
/// <typeparam name="TEntity">Type of an entity object</typeparam>
public interface IStorable<TEntity> where TEntity : IEntity
{
    public Guid Id { get; }
    public bool Virtual => false;
    
    public bool NotSave(TEntity? entity)
    {
        if (entity == null) return false;
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        return Virtual;
    }
    
    public bool NotLoad(TEntity? entity)
    {
        if (entity == null) return true;
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        return Virtual;
    }
    
    public TEntity? Save(TEntity? entity = default, Action<IEntity>? setAddedState = null, bool upcome = true);

    public TEntity SaveNotNull(TEntity? entity = default, Action<IEntity>? setAddedState = null, bool upcome = true)
    {
        return Save(entity, setAddedState, upcome) ?? throw new NullReferenceException(Lang.Exceptions.IStorable.NullSave);
    }
    
    public IStorable<TEntity>? Load(TEntity entity, bool upcome = true);
    
    public IStorable<TEntity> LoadNotNull(TEntity entity, bool upcome = true)
    {
        return Load(entity, upcome) ?? throw new NullReferenceException(Lang.Exceptions.IStorable.NullLoad);
    }
}