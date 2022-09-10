using Rnd.Api.Data.Entities;

namespace Rnd.Api.Data;

// ReSharper disable once TypeParameterCanBeVariant
public interface IStorable<TEntity> where TEntity : IEntity
{
    public void Save(TEntity entity);
    public void Load(TEntity entity);
}