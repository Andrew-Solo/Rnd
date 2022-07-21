using RnDBot.Models.Character.Fields;
using RnDBot.Models.Character.Panels;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class Character<TDomain, TSkill> : AbstractCharacter 
    where TDomain : struct 
    where TSkill : struct
{
    public Character(ICharacter character, List<Domain<TDomain, TSkill>> domains) : base(character)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains);
    }

    public Character(string name, List<Domain<TDomain, TSkill>> domains) : base(name)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains);
    }

    public Domains<TDomain, TSkill> Domains { get; }

    public override List<IPanel> Panels => new()
    {
        General,
        Leveling,
        Pointers,
        Attributes,
        Domains,
    };
}