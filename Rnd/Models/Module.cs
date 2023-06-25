using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Results;

namespace Rnd.Models;

[Index(nameof(Name), nameof(Version), IsUnique = true)]
public class Module : Model
{
    [MaxLength(TextSize.Tiny)]
    public Version Version { get; protected set; }
    
    public Guid CreatorId { get; protected set; }
    public virtual User Creator { get; protected set; } = null!;
    
    public Guid? MainId { get; protected set; }
    public virtual Unit? Main { get; protected set; }
    public virtual List<Unit> Units { get; } = new();
    public virtual List<Space> Spaces { get; } = new();
    
    public bool System { get; protected set; }
    public bool Default { get; protected set; }
    public bool Hidden { get; protected set; }
    
    protected Module(Guid creatorId, Version? version)
    {
        CreatorId = creatorId;
        Version = version ?? new Version("0.1.0");
    }
    
    public static Result<Module> Create(ModuleData data)
    {
        Guard.Against.Null(data.CreatorId);
        
        var module = new Module(data.CreatorId.Value, data.Version);
        
        module.FillData(data);
        
        return Result.Ok(module);
    }

    protected override void FillData(ModelData data)
    {
        base.FillData(data);
        var moduleData = (ModuleData) data;
        MainId = moduleData.MainId;
        if (moduleData.System != null) System = moduleData.System.Value;
        if (moduleData.Default != null) Default = moduleData.Default.Value;
        if (moduleData.Hidden != null) Hidden = moduleData.Hidden.Value;
    }
    
    public override ExpandoObject Get()
    {
        dynamic view = base.Get();
        
        view.version = Version;
        view.creatorId = CreatorId;
        view.mainId = MainId!;
        view.system = System;
        view.@default = Default;
        view.hidden = Hidden;
        
        return view;
    }
}