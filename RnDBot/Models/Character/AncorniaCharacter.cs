using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Character.Panels;
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

    [JsonConstructor]
    public AncorniaCharacter(string name, General general, Attributes attributes, Pointers pointers, Effects effects, Traumas traumas,
        Domains<AncorniaDomainType, AncorniaSkillType> domains)
        : base(name, general, attributes, pointers, effects, traumas, domains) 
    {}
}