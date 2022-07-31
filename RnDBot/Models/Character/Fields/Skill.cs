using Newtonsoft.Json;
using RnDBot.Models.Glossaries;

namespace RnDBot.Models.Character.Fields;

public class Skill<TSkill> where TSkill : struct
{
    public Skill(AttributeType coreAttribute, TSkill skillType, int value = 0)
    {
        CoreAttribute = coreAttribute;
        SkillType = skillType;
        Value = value;
        Modified = false;
    }
    
    public AttributeType CoreAttribute { get; set; }
    public TSkill SkillType { get; set; }
    public int Value { get; set; }
    
    [JsonIgnore]
    public bool Modified { get; set; }
    
    [JsonIgnore]
    public string Name => Glossary.GetSkillName(SkillType) + (Modified ? "*" : "");
}