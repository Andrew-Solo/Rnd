using Dice;
using Newtonsoft.Json;
using RnDBot.Models.Glossaries;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Models.Character.Panels.Effect;

//TODO Прогрессирование травм
//TODO Сплит травм
//TODO Лечение травм
public class TraumaEffect : IEffect
{
    public TraumaEffect(TraumaType traumaType, DamageType damageType, int number = 0)
    {
        TraumaType = traumaType;
        DamageType = damageType;
        Number = number;

        TraumaState = TraumaState.Unstable;
        Fines = CreateFines(traumaType);
    }

    [JsonConstructor]
    public TraumaEffect(TraumaType traumaType, DamageType damageType, TraumaState traumaState, Dictionary<AttributeType, int> fines, 
        int number = 0)
    {
        TraumaType = traumaType;
        DamageType = damageType;
        TraumaState = traumaState;
        Fines = fines;
        Number = number;
    }

    public TraumaType TraumaType { get; }
    public DamageType DamageType { get; }
    public TraumaState TraumaState { get; set; }
    public Dictionary<AttributeType, int> Fines { get; }

    public int Number { get; set; }

    [JsonIgnore]
    public string Name => Glossary.GetTraumaName(TraumaType, DamageType, TraumaState) + (Number == 0 ? "" : $" #{Number}");

    [JsonIgnore] 
    public List<AttributeEffect> AttributeEffects => GetEffects();

    [JsonIgnore]
    public string Highlighter => TraumaState switch
    {
        TraumaState.Unstable => "**",
        TraumaState.Stable => "*",
        TraumaState.Chronic => "",
        _ => throw new ArgumentOutOfRangeException()
    };

    [JsonIgnore]
    public string View => $"{Highlighter}{Name}{Highlighter} \n> " + String.Join("\n> ", AttributeEffects.Select(e => e.View));

    public void ModifyAttribute(Attribute attribute)
    {
        foreach (var attributeEffect in AttributeEffects)
        {
            attributeEffect.ModifyAttribute(attribute);
        }
    }

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

    private List<AttributeEffect> GetEffects()
    {
        var statedFines = GetStatedFines();
        var effects = new List<AttributeEffect>();
        
        foreach (var (type, value) in Fines)
        {
            effects.Add(new AttributeEffect(Glossary.GetTraumaEffectName(DamageType, type, value), type, statedFines[type]));
        }

        return effects;
    }

    private Dictionary<AttributeType, int> GetStatedFines()
    {
        if (TraumaState == TraumaState.Unstable) return Fines;
        
        var statedFines = new Dictionary<AttributeType, int>(Fines.OrderBy(f => f.Value));

        for (int i = 0; i < 2; i++)
        {
            var odd = 0;

            foreach (var (type, value) in statedFines)
            {
                if (value % 2 != 0) odd++;

                statedFines[type] = value / 2;
            }

            for (int j = 0; j < odd / 2; j++)
            {
                var (type, _) = statedFines.ElementAt(j);
                statedFines[type]--;
            }

            if (TraumaState != TraumaState.Chronic)
            {
                break;
            }
        }
        
        return statedFines;
    }
}