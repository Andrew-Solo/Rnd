using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Logic.Helpers;
using Rnd.Api.Logic.Localization;

namespace Rnd.Api.Logic.Models.Resources;

public class Resource : IResource, IStorable<Data.Entities.Resource>
{
    public Resource(Guid id, string group, string name)
    {
        Id = id;
        Path = group;
        Name = name;
    }

    public Guid Id { get; }
    public string? Path { get; private set; }
    public string Name { get; private set; }
    
    public decimal Default => 0;
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    
    #region IStorable

    public void Save(Data.Entities.Resource entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Default = Default;
        entity.Value = Value;
        entity.Min = Min;
        entity.Max = Max;
    }

    public void Load(Data.Entities.Resource entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = entity.Value;
        Min = entity.Min;
        Max = entity.Max;
    }

    #endregion
}