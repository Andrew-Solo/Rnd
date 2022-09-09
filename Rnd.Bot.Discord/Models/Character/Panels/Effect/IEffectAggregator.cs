using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Glossaries;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

public interface IEffectAggregator
{
    public List<PowerEffect> PowerEffects { get; }
    public List<AttributeEffect> AttributeEffects { get; }
    public List<PointEffect> PointEffects { get; }
    public List<DomainEffect<AncorniaDomainType>> DomainEffects { get; }
    public List<SkillEffect<AncorniaSkillType>> SkillEffects { get; }
    public List<AggregateEffect> AggregateEffects { get; }
    
    [JsonIgnore]
    public IReadOnlyCollection<IEffect> EffectList => AttributeEffects.Cast<IEffect>().Union(PowerEffects).Union(PointEffects)
        .Union(DomainEffects).Union(SkillEffects).Union(AggregateEffects).ToList();

    public void RemoveEffect(IEffect effect)
    {
        switch (effect)
        {
            case PowerEffect powerEffect:
                PowerEffects.Remove(powerEffect);
                break;
            case AttributeEffect attributeEffect:
                AttributeEffects.Remove(attributeEffect);
                break;
            case PointEffect pointEffect:
                PointEffects.Remove(pointEffect);
                break;
            case DomainEffect<AncorniaDomainType> domainEffect:
                DomainEffects.Remove(domainEffect);
                break;
            case SkillEffect<AncorniaSkillType> skillEffect:
                SkillEffects.Remove(skillEffect);
                break;
            case AggregateEffect aggregateEffect:
                AggregateEffects.Remove(aggregateEffect);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void AddEffect(IEffect effect)
    {
        switch (effect)
        {
            case PowerEffect powerEffect:
                PowerEffects.Add(powerEffect);
                break;
            case AttributeEffect attributeEffect:
                AttributeEffects.Add(attributeEffect);
                break;
            case PointEffect pointEffect:
                PointEffects.Add(pointEffect);
                break;
            case DomainEffect<AncorniaDomainType> domainEffect:
                DomainEffects.Add(domainEffect);
                break;
            case SkillEffect<AncorniaSkillType> skillEffect:
                SkillEffects.Add(skillEffect);
                break;
            case AggregateEffect aggregateEffect:
                AggregateEffects.Add(aggregateEffect);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public IEffect GetEffect(string name)
    {
        return EffectList.First(e => e.Name == name);
    }
}