namespace Rnd.Api.Modules.Basic.Resources;

public class StrictResource : Resource
{
    public StrictResource(string name) : base(name) { }

    public new decimal Min
    {
        get => base.Min.GetValueOrDefault();
        set => base.Min = value;
    }

    public new decimal Max
    {
        get => base.Max.GetValueOrDefault();
        set => base.Max = value;
    }
}