namespace Rnd.Api.Modules.Basic.Resources;

public class DefaultResource : Resource
{
    public DefaultResource(string name, Func<DefaultResource, decimal>? getDefaultValue, 
        Func<DefaultResource, decimal>? getDefaultMin = null, 
        Func<DefaultResource, decimal>? getDefaultMax = null) 
        : base(name)
    {
        GetDefaultMin = getDefaultMin ?? (_ => 0);
        GetDefaultMax = getDefaultMax ?? (_ => Decimal.MaxValue);
        GetDefaultValue = getDefaultValue ?? (r => r.Min);
        
        Value = GetDefaultValue(this);
    }

    public new decimal Min
    {
        get => base.Min ?? GetDefaultMin(this);
        set => base.Min = value;
    }
    
    public new decimal Max
    {
        get => base.Min ?? GetDefaultMax(this);
        set => base.Min = value;
    }

    public Func<DefaultResource, decimal> GetDefaultValue { get; set; }
    public Func<DefaultResource, decimal> GetDefaultMin { get; set; }
    public Func<DefaultResource, decimal> GetDefaultMax { get; set; }
}