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

    public Domains<TDomain, TSkill> Domains { get; }

    public override List<IPanel> Panels => new()
    {
        General,
        Pointers,
        Attributes,
        Domains,
    };
}