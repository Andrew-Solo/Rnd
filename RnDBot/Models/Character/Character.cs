using RnDBot.Models.CharacterFields;

namespace RnDBot.Models.Character;

public class Character<TDomain, TSkill> : ICharacter 
    where TDomain : struct 
    where TSkill : struct
{
    public Character(string name, List<Domain<TDomain, TSkill>> domains)
    {
        Leveling = new Leveling(this);
        General = new General(this, name);
        Conditions = new Conditions(this);
        Attributes = new Attributes(this);
        Domains = new Domains<TDomain, TSkill>(this, domains);
    }

    public General General { get; }
    public Leveling Leveling { get; }
    public Conditions Conditions { get; }
    public Attributes Attributes { get; }
    public Domains<TDomain, TSkill> Domains { get; }
}