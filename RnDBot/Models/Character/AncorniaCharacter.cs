using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;

namespace RnDBot.Models.Character;

/// <summary>
/// Alias class for Character&#60;AncorniaDomainType, AncorniaSkillType&#62; generic
/// </summary>
public class AncorniaCharacter : Character<AncorniaDomainType, AncorniaSkillType>
{
    public AncorniaCharacter(ICharacter character, List<Domain<AncorniaDomainType, AncorniaSkillType>> domains) 
        : base(character, domains)
    { }

    public AncorniaCharacter(string name, List<Domain<AncorniaDomainType, AncorniaSkillType>> domains) 
        : base(name, domains)
    { }
}