using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Skill : Int32Parameter
{
    public Skill(SkillType skillType) : base(skillType.ToString())
    {
        SkillType = skillType;
    }
    
    public SkillType SkillType { get; }
    public override string Path => nameof(Skill);
}