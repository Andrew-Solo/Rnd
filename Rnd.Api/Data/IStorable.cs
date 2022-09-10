using Rnd.Api.Data.Entities;

namespace Rnd.Api.Data;

// ReSharper disable once TypeParameterCanBeVariant
/// <summary>
/// The interface for synchronizing a state of this object with an entity object.
/// Used to work with a database through the entities.
/// </summary>
/// <typeparam name="TEntity">Type of an entity object</typeparam>
public interface IStorable<TEntity> where TEntity : IEntity, new()
{
    public Guid Id { get; }

    /// <summary>
    /// Saves the state of the current object to an entity object.
    /// </summary>
    /// <param name="entity">State storage entity</param>
    public void Save(TEntity entity);

    /// <summary>
    /// Loads the state from the entity object into the current object.
    /// </summary>
    /// <param name="entity">State storage entity</param>
    public void Load(TEntity entity);

    /// <summary>
    /// Creates a new entity that stores the current state of this object.
    /// </summary>
    /// <returns>State storage entity</returns>
    public TEntity CreateEntity()
    {
        var entity = new TEntity
        {
            Id = Id
        };
        
        Save(entity);
        
        return entity;
    }
}