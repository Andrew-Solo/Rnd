namespace Rnd.Api.Data;

public static class Extensions
{
    public static void SaveList<TEntity>(this List<TEntity> entities, List<IStorable<TEntity>> storables, Action<IEntity>? setAddedState = null) 
        where TEntity : IEntity
    {
        var storableIds = storables
            .Where(s => !s.NotSave(entities.FirstOrDefault(e => e.Id == s.Id)))
            .Select(s => s.Id).ToList();

        var entityIds = entities.Select(e => e.Id).ToList();
        
        var deleteIds = entityIds.Except(storableIds).ToList();
        var updateIds = entityIds.Intersect(storableIds).ToList();
        var insertIds = storableIds.Except(entityIds).ToList();

        deleteIds.ForEach(id => entities.Remove(entities.First(e => e.Id == id)));
        
        entities
            .Where(e => updateIds.Contains(e.Id)).ToList()
            .ForEach(e => storables.First(s => s.Id == e.Id).Save(e, setAddedState));

        var insertEntities = storables
            .Where(s => insertIds.Contains(s.Id))
            .Select(s => s.SaveNotNull(default, setAddedState)).ToList();
        
        entities.AddRange(insertEntities);
        
        if (setAddedState == null) return;

        insertEntities.ForEach(e => setAddedState(e));
    }
    
    public static List<IStorable<TEntity>> LoadList<TEntity>(this List<IStorable<TEntity>> storables, List<TEntity> entities, IStorableFactory<TEntity> factory) 
        where TEntity : IEntity
    {
        var storableIds = storables
            .Where(s => !s.NotLoad(entities.FirstOrDefault(e => e.Id == s.Id)))
            .Select(s => s.Id).ToList();

        var entityIds = entities.Select(e => e.Id).ToList();
        
        var deleteIds = storableIds.Except(entityIds).ToList();
        var updateIds = entityIds.Intersect(storableIds).ToList();
        var insertIds = entityIds.Except(storableIds).ToList();

        deleteIds.ForEach(id => storables.Remove(storables.First(s => s.Id == id)));
        
        storables
            .Where(s => updateIds.Contains(s.Id)).ToList()
            .ForEach(s => s.Load(entities.First(e => e.Id == s.Id)));

        storables.AddRange(
            entities
                .Where(e => insertIds.Contains(e.Id))
                .Select(factory.CreateStorable));

        return storables;
    }
}