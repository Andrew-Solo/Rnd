using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Fields;

public class Domain<TDomain, TSkill> : IField
    where TDomain : struct
    where TSkill : struct
{
    public Domain(TDomain domainType, List<Skill<TSkill>> skills, int domainLevel = 4)
    {
        DomainType = domainType;
        Skills = skills;
        DomainLevel = domainLevel;
    }

    public TDomain DomainType { get; set; }
    public int DomainLevel { get; set; }
    
    //TODO Индексатор
    public List<Skill<TSkill>> Skills { get; }
    
    public string Name => Glossary.GetDomainName(DomainType) + $" [{DomainLevel}]";

    public object Value =>
        Skills.ToDictionary(
            skill => Glossary.GetSkillName(skill.SkillType),
            skill => (skill.Value + DomainLevel).ToString());
    
    public ValueType Type => ValueType.Dictionary;
    public bool IsInline => true;
}