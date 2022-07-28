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

    public ICharacter Character { get; }
    public List<Domain<TDomain, TSkill>> CoreDomains { get; }
    
    //TODO Items
    public List<Domain<TDomain, TSkill>> FinalDomains => CoreDomains;

    public string Title => "Навыки";
    public List<IField> Fields => FinalDomains.Select(a => (IField) a).ToList();
    public string Footer => Character.GetFooter;
}