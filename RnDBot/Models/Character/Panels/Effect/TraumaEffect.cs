using Dice;
using Newtonsoft.Json;
using RnDBot.Models.Glossaries;

namespace RnDBot.Models.Character.Panels.Effect;

public class TraumaEffect : AggregateEffect
{
    public TraumaEffect(TraumaType traumaType, DamageType damageType) 
        : base(Glossary.GetTraumaName(traumaType, damageType, TraumaState.Unstable))
    {
        TraumaType = traumaType;
        DamageType = damageType;
        
        TraumaState = TraumaState.Unstable;
        Fines = CreateFines(traumaType);
    }

    [JsonConstructor]
    public TraumaEffect(TraumaType traumaType, DamageType damageType, TraumaState traumaState, Dictionary<AttributeType, int> fines) 
        : base(Glossary.GetTraumaName(traumaType, damageType, traumaState), 
            new List<PowerEffect>(), GetEffects(fines, damageType), 
            new List<PointEffect>(), new List<DomainEffect<AncorniaDomainType>>(), 
            new List<SkillEffect<AncorniaSkillType>>())
    {
        TraumaType = traumaType;
        DamageType = damageType;
        TraumaState = traumaState;
        Fines = fines;
    }
    
    public TraumaType TraumaType { get; }
    public DamageType DamageType { get; }
    public TraumaState TraumaState { get; }
    public Dictionary<AttributeType, int> Fines { get; }

    public override string Name => Glossary.GetTraumaName(TraumaType, DamageType, TraumaState);

    public override List<AttributeEffect> AttributeEffects => GetEffects(Fines, DamageType);

    private static Dictionary<AttributeType, int> CreateFines(TraumaType type)
    {
        var (removeAmount, fineAmount) = type switch
        {
            TraumaType.Deadly => (4, 16),
            TraumaType.Critical => (5, 8),
            TraumaType.Heavy => (6, 4),
            TraumaType.Light => (7, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        var attributes = Glossary.AttributeNames.Keys.ToList();

        for (var i = 0; i < removeAmount; i++)
        {
            var random = (int) Roller.Roll($"1d{8 - i}").Value - 1;
            attributes.Remove(attributes[random]);
        }

        var fines = new Dictionary<AttributeType, int>();

        for (int i = 0; i < fineAmount; i++)
        {
            var random = (int) Roller.Roll($"1d{attributes.Count}").Value - 1;
            
            if (!fines.ContainsKey(attributes[random]))
            {
                fines[attributes[random]] = -1;
            }
            else
            {
                fines[attributes[random]]--;
            }
            
            if (fines[attributes[random]] <= -8) attributes.Remove(attributes[random]);
        }

        return fines;
    }

    private static List<AttributeEffect> GetEffects(Dictionary<AttributeType, int> fines, DamageType damageType)
    {
        var activeFines = fines.Where(pair => pair.Value != 0);
        var effects = new List<AttributeEffect>();
        
        foreach (var (type, value) in activeFines)
        {
            effects.Add(new AttributeEffect(Glossary.GetTraumaEffectName(damageType, type, value), type, value));
        }

        return effects;
    }
}