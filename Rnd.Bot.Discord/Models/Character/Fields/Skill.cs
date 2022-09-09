using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Glossaries;

namespace Rnd.Bot.Discord.Models.Character.Fields;

public class Skill<TSkill> where TSkill : struct
{
    public Skill(TSkill skillType, int value = 0)
    {
        SkillType = skillType;
        Value = value;
        Modified = false;
    }

    [JsonIgnore] 
    public AttributeType CoreAttribute => Glossary.GetSkillCoreAttribute(SkillType);
    public TSkill SkillType { get; set; }
    public int Value { get; set; }
    
    [JsonIgnore]
    public bool Modified { get; set; }
    
    [JsonIgnore]
    public string Name => Glossary.GetSkillName(SkillType) + (Modified ? "*" : "");
}