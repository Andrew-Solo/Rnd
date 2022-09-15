using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class FinalSkill : Skill
{
    public FinalSkill(ICharacter character, Skill original) : base(character, original.SkillType, original.Value)
    {
        
    }

    public override string Path => Helpers.Path.Combine(nameof(Final), base.Path);
    public override bool Virtual => true;
}