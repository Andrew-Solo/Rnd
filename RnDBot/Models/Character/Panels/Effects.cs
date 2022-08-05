using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RnDBot.Models.Character.Panels.Effect;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels;

public class Effects : IPanel, IValidatable, IEffectAggregator, IEffectProvider
{
    public Effects(ICharacter character)
    {
        Character = character;
        PowerEffects = new List<PowerEffect>();
        AttributeEffects = new List<AttributeEffect>();
        PointEffects = new List<PointEffect>();
        DomainEffects = new List<DomainEffect<AncorniaDomainType>>();
        SkillEffects = new List<SkillEffect<AncorniaSkillType>>();
        AggregateEffects = new List<AggregateEffect>();
    }
    
    [JsonConstructor]
    public Effects(ICharacter character, List<PowerEffect> powerEffects, List<AttributeEffect> attributeEffects, 
        List<PointEffect> pointEffects, List<DomainEffect<AncorniaDomainType>> domainEffects, 
        List<SkillEffect<AncorniaSkillType>> skillEffects, List<AggregateEffect> aggregateEffects)
    {
        Character = character;
        PowerEffects = powerEffects;
        AttributeEffects = attributeEffects;
        PointEffects = pointEffects;
        DomainEffects = domainEffects;
        SkillEffects = skillEffects;
        AggregateEffects = aggregateEffects;
    }

    [JsonIgnore]
    public ICharacter Character;
    
    public List<PowerEffect> PowerEffects { get; }
    public List<AttributeEffect> AttributeEffects { get; }
    public List<PointEffect> PointEffects { get; }
    public List<DomainEffect<AncorniaDomainType>> DomainEffects { get; }
    public List<SkillEffect<AncorniaSkillType>> SkillEffects { get; }
    public List<AggregateEffect> AggregateEffects { get; }

    [JsonIgnore] 
    public IReadOnlyCollection<IEffect> CoreEffects => ((IEffectAggregator) this).EffectList;
    
    public IEnumerable<IEffect> GetEffects() => CoreEffects;

    [JsonIgnore]
    public IReadOnlyCollection<IEffect> FinalEffects
    {
        get
        {
            var result = new List<IEffect>();
            
            Character.Panels
                .OfType<IEffectProvider>().ToList()
                .ForEach(ep => result.AddRange(ep.GetEffects()));

            return result;
        }
    }

    [JsonIgnore]
    public string Title => "Эффекты";
    
    [JsonIgnore]
    public string Description => EmbedView.Build(CoreEffects.Select(e => e.View).ToArray(), ValueType.List);
    
    [JsonIgnore] 
    public string Footer => Character.GetFooter;

    [JsonIgnore]
    public bool IsValid
    {
        get
        {
            var valid = true;
            var errors = new List<string>();

            foreach (var grouping in CoreEffects.GroupBy(e => e.Name))
            {
                if (grouping.Count() > 1)
                {
                    valid = false;
                    errors.Add("Эффекты не могут иметь одинаковые имена");
                }

                if (!Regex.IsMatch(grouping.Key, @"^[a-zA-Zа-я-А-Я 0-9]*$"))
                {
                    valid = false;
                    errors.Add("Имя эффекта должно состоять из латиницы, кириллицы, цифр или пробелов.");
                }
            }

            Errors = errors.ToArray();
            return valid;
        }
    }

    [JsonIgnore]
    public string[]? Errors { get; private set; }
}