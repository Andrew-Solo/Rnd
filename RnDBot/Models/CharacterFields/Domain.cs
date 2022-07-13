using RnDBot.Models.Glossaries;
using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.CharacterFields;

public class Domain<TDomain, TSkill> : IField
    where TDomain : struct
    where TSkill : struct
{
    public Domain(SettingType settingType, TDomain domainType, Dictionary<Skill<TSkill>, int> skills, int? domainLevel = null)
    {
        SettingType = settingType;
        DomainType = domainType;
        Skills = skills;
        DomainLevel = domainLevel;
    }

    public SettingType SettingType { get; set; }
    public TDomain DomainType { get; set; }
    public int? DomainLevel { get; set; }
    public Dictionary<Skill<TSkill>, int> Skills { get; }
    
    public string Name => Glossary.GetDomainDictionaryValue(DomainType) + DomainLevelPostfix;
    public object? Value => Skills.ToDictionary(pair => Glossary.GetSkillDictionaryValue(pair.Key.SkillType), pair => pair.Value.ToString());
    public ValueType Type => ValueType.Dictionary;
    public bool IsInline => false;
    
    public string DomainLevelPostfix => 
        DomainLevel == null 
            ? "" 
            : $" [{DomainLevel}]";
}