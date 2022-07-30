using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

public class Domains<TDomain, TSkill> : IPanel 
    where TDomain : struct 
    where TSkill : struct
{
    public Domains(ICharacter character, List<Domain<TDomain, TSkill>> coreDomains)
    {
        Character = character;
        CoreDomains = coreDomains;
    }

    [JsonIgnore]
    public ICharacter Character { get; }

    [JsonIgnore] public int MaxSkillLevel => (int) Math.Floor((double) Character.Attributes.Power.Max / 8) + 6;
    
    //TODO Индексатор
    public List<Domain<TDomain, TSkill>> CoreDomains { get; }
    
    [JsonIgnore]
    public List<Skill<TSkill>> AllCoreSkills
    {
        get
        {
            var result = new List<Skill<TSkill>>();
            
            CoreDomains.ForEach(d => result.AddRange(d.Skills));

            return result;
        }
    }

    //TODO Items
    [JsonIgnore]
    public List<Domain<TDomain, TSkill>> FinalDomains => CoreDomains;
    
    [JsonIgnore]
    public List<Skill<TSkill>> AllFinalSkills
    {
        get
        {
            var result = new List<Skill<TSkill>>();
            
            FinalDomains.ForEach(d => result.AddRange(d.Skills));

            return result;
        }
    }

    [JsonIgnore]
    public string Title => "Навыки";
    
    [JsonIgnore]
    public List<IField> Fields => FinalDomains.Select(a => (IField) a).ToList();
    
    [JsonIgnore]
    public string Footer => Character.GetFooter;
}