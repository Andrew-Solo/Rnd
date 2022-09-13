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
        if (entity?.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        return Virtual;
    }
    
    public bool NotLoad(TEntity? entity)
    {
        if (entity?.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        return Virtual;
    }

    /// <summary>
    /// Saves the state of the current object to an entity object.
    /// </summary>
    /// <param name="entity">State storage entity</param>
    public TEntity? Save(TEntity? entity);

    public TEntity SaveNotNull(TEntity? entity)
    {
        return Save(entity) ?? throw new NullReferenceException(Lang.Exceptions.IStorable.NullNested);
    }

    /// <summary>
    /// Loads the state from the entity object into the current object.
    /// </summary>
    /// <param name="entity">State storage entity</param>
    public void Load(TEntity entity);
}