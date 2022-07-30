using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RnDBot.Models.Character.Panels.Effect;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels;

public class Effects : IPanel, IValidatable
{
    public Effects(ICharacter character)
    {
        Character = character;
        AttributeEffects = new List<AttributeEffect>();
        PointEffects = new List<PointEffect>();
        SkillEffects = new List<SkillEffect<AncorniaSkillType>>();
    }
    
    [JsonConstructor]
    public Effects(ICharacter character, List<AttributeEffect> attributeEffects, List<PointEffect> pointEffects, 
        List<SkillEffect<AncorniaSkillType>> skillEffects)
    {
        Character = character;
        AttributeEffects = attributeEffects;
        PointEffects = pointEffects;
        SkillEffects = skillEffects;
    }

    [JsonIgnore]
    public ICharacter Character;
    
    public List<AttributeEffect> AttributeEffects { get; }
    public List<PointEffect> PointEffects { get; }
    public List<SkillEffect<AncorniaSkillType>> SkillEffects { get; }

    [JsonIgnore] 
    public IReadOnlyCollection<IEffect> CoreEffects => AttributeEffects.Cast<IEffect>().Union(PointEffects).Union(SkillEffects).ToList();

    public void RemoveEffect(IEffect effect)
    {
        switch (effect)
        {
            case AttributeEffect attributeEffect:
                AttributeEffects.Remove(attributeEffect);
                break;
            case PointEffect pointEffect:
                PointEffects.Remove(pointEffect);
                break;
            case SkillEffect<AncorniaSkillType> skillEffect:
                SkillEffects.Remove(skillEffect);
                break;
        }
    }
    
    [JsonIgnore]
    public string Title => "Эффекты";
    
    [JsonIgnore]
    public string Description => EmbedView.Build(CoreEffects.Select(e => e.View).ToArray(), ValueType.List);

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