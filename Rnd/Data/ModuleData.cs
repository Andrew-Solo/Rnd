namespace Rnd.Data;

public class ModuleData : ModelData
{
    public Version? Version => this[nameof(Version)].GetVersionOrNull();
    public Guid? CreatorId => this[nameof(CreatorId)].GetGuidOrNull();
    public Guid? MainId => this[nameof(MainId)].GetGuidOrNull();
    public bool? System => this[nameof(System)].GetBooleanOrNull();
    public bool? Default => this[nameof(Default)].GetBooleanOrNull();
    public bool? Hidden => this[nameof(Hidden)].GetBooleanOrNull();
}