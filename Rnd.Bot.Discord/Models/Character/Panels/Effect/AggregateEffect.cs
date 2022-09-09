using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Character.Fields;
using Rnd.Bot.Discord.Models.Common;
using Rnd.Bot.Discord.Models.Glossaries;
using Attribute = Rnd.Bot.Discord.Models.Character.Fields.Attribute;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

public class AggregateEffect : IEffect, IEffectAggregator
{
    public AggregateEffect(string name)
    {
        Name = name;
        PowerEffects = new List<PowerEffect>();
        AttributeEffects = new List<AttributeEffect>();
        PointEffects = new List<PointEffect>();
        DomainEffects = new List<DomainEffect<AncorniaDomainType>>();
        SkillEffects = new List<SkillEffect<AncorniaSkillType>>();
    }
    
    [JsonConstructor]
    public AggregateEffect(string name, List<PowerEffect> powerEffects, List<AttributeEffect> attributeEffects, 
        List<PointEffect> pointEffects, List<DomainEffect<AncorniaDomainType>> domainEffects, 
        List<SkillEffect<AncorniaSkillType>> skillEffects)
    {
        Name = name;
        PowerEffects = powerEffects;
        AttributeEffects = attributeEffects;
        PointEffects = pointEffects;
        DomainEffects = domainEffects;
        SkillEffects = skillEffects;
    }

    public string Name { get; }
    
    public List<PowerEffect> PowerEffects { get; }
    public List<AttributeEffect> AttributeEffects { get; }
    public List<PointEffect> PointEffects { get; }
    public List<DomainEffect<AncorniaDomainType>> DomainEffects { get; }
    public List<SkillEffect<AncorniaSkillType>> SkillEffects { get; }
    
    [JsonIgnore]
    public List<AggregateEffect> AggregateEffects => new();

    [JsonIgnore]
    public List<IEffect> Effects => ((IEffectAggregator) this).EffectList.ToList();

    public void ModifyPower(CounterField power)
    {
        Effects.ForEach(e => e.ModifyPower(power));
    }

    public void ModifyAttribute(Attribute attribute)
    {
        Effects.ForEach(e => e.ModifyAttribute(attribute));
    }

    public void ModifyPointer(Pointer pointer)
    {
        Effects.ForEach(e => e.ModifyPointer(pointer));
    }

    public void ModifyDomain<TDomain, TSkill>(Domain<TDomain, TSkill> domain) 
        where TDomain : struct where TSkill : struct
    {
        Effects.ForEach(e => e.ModifyDomain(domain));
    }

    public void ModifySkill<TSkill>(Skill<TSkill> skill) 
        where TSkill : struct
    {
        Effects.ForEach(e => e.ModifySkill(skill));
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** \n> " + String.Join("\n> ", Effects.Select(e => e.View));
}