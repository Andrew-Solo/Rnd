using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class Skill : Int32Parameter
{
    public Skill(ICharacter character, SkillType skillType, int? value = null) : base(character, skillType.ToString())
    {
        SkillType = skillType;
        Value = value ?? Default;
    }
    
    public SkillType SkillType { get; }
    public override string Path => nameof(Skill);
    
    public const int Default = 0;
}