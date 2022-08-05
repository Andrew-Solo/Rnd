using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels.Effect;

public class DomainEffect<TEffectDomain> : IEffect
{
    public DomainEffect(string name, TEffectDomain domainType, int modifier = 0)
    {
        Name = name;
        DomainType = domainType;
        Modifier = modifier;
    }

    public string Name { get; }
    public TEffectDomain DomainType { get; }
    public int Modifier { get; }
    
    public void ModifyDomain<TDomain, TSkill>(Domain<TDomain, TSkill> domain) 
        where TDomain : struct where TSkill : struct
    {
        //TODO Убрать костыль с глоссарием и сравнивать напрямую
        if (DomainType is not TDomain type) return;
        if (Glossary.GetDomainName(domain.DomainType) != Glossary.GetDomainName(type)) return;

        domain.DomainLevel += Modifier;
        domain.Modified = true;
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** {Glossary.GetDomainName(DomainType)} " +
                          $"`{EmbedView.Build(Modifier, ValueType.InlineModifier)}`";
}