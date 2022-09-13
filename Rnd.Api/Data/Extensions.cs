using Rnd.Api.Localization;

namespace Rnd.Api.Data;

public static class Extensions
{
    public static void SaveList<TEntity>(this List<TEntity> entities, List<IStorable<TEntity>> storable) 
        where TEntity : IEntity
    {
        var storableIds = storable
            .Where(c => c.NotSave(entities.FirstOrDefault(ec => ec.Id == c.Id)))
            .Select(c => c.Id).ToList();

        var entitiesIds = entities.Select(ec => ec.Id).ToList();
        
        var deleteIds = entitiesIds.Except(storableIds).ToList();
        var updateIds = entitiesIds.Intersect(storableIds).ToList();
        var insertIds = storableIds.Except(entitiesIds).ToList();

        deleteIds.ForEach(id => entities.Remove(entities.First(ec => ec.Id == id)));
        
        entities
            .Where(ec => updateIds.Contains(ec.Id)).ToList()
            .ForEach(ec => storable.First(c => c.Id == ec.Id).Save(ec));

        entities.AddRange(
            storable
                .Where(c => insertIds.Contains(c.Id))
                .Select(c => c.SaveNotNull(default)));
    }
}