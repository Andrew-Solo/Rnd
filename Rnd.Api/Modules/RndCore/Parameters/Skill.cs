using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Skill : Int32Parameter
{
    public Skill(SkillType skillType, int? value = null) : base(skillType.ToString())
    {
        SkillType = skillType;
        Value = value ?? Default;
    }
    
    public SkillType SkillType { get; }
    public override string Path => nameof(Skill);
    
    public const int Default = 0;
}