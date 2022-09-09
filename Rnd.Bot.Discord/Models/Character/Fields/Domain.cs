using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Glossaries;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Fields;

public class Domain<TDomain, TSkill> : IField
    where TDomain : struct
    where TSkill : struct
{
    public Domain(TDomain domainType, List<Skill<TSkill>> skills, int domainLevel = 4)
    {
        DomainType = domainType;
        Skills = skills;
        DomainLevel = domainLevel;
        Modified = false;
    }

    public TDomain DomainType { get; set; }
    public int DomainLevel { get; set; }
    
    [JsonIgnore]
    public bool Modified { get; set; }
    
    public List<Skill<TSkill>> Skills { get; }
    
    [JsonIgnore]
    public string Name => Glossary.GetDomainName(DomainType) + (Modified ? "*" : "") + $" [{DomainLevel}]";
    
    [JsonIgnore]
    public object Value =>
        Skills.ToDictionary(
            skill => skill.Name,
            skill => skill.Value.ToString());
    
    [JsonIgnore]
    public ValueType Type => ValueType.Dictionary;
    
    [JsonIgnore]
    public bool IsInline => true;
}