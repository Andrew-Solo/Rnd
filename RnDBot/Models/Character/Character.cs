using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

public class Character<TDomain, TSkill> : AbstractCharacter 
    where TDomain : struct 
    where TSkill : struct
{
    public Character(ICharacter character, List<Domain<TDomain, TSkill>> domains) : base(character)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains);
    }
    
    [JsonConstructor]
    public Character(string name, General general, Attributes attributes, Pointers pointers, Effects effects, Domains<TDomain, TSkill> domains) 
        : base(name, general, attributes, pointers, effects)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains.CoreDomains);
    }

    public Domains<TDomain, TSkill> Domains { get; }

    public override int GetPower => Domains.CoreSkills.Sum(s => s.Value);

    [JsonIgnore]
    public override List<IPanel> Panels => new()
    {
        General,
        Pointers,
        Attributes,
        Domains,
        Effects,
    };
}