namespace Rnd.Api.Data;

public interface IStorableFactory<TEntity> where TEntity : IEntity
{
    IStorable<TEntity> CreateStorable(TEntity entity);
}