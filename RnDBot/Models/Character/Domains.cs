using RnDBot.Models.CharacterFields;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class Domains<TDomain, TSkill> : IPanel 
    where TDomain : struct 
    where TSkill : struct
{
    public Domains(Character character, List<Domain<TDomain, TSkill>> coreDomains)
    {
        Character = character;
        CoreDomains = FinalDomains = coreDomains;
    }

    public Character Character { get; }
    public List<Domain<TDomain, TSkill>> CoreDomains { get; }
    public List<Domain<TDomain, TSkill>> FinalDomains { get; }

    public string Title => "Навыки";
    public List<IField> Fields => FinalDomains.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}