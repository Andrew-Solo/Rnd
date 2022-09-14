using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class Skill : Int32Parameter
{
    public Skill(IEntity entity) : base(entity) { }
    
    public Skill(ICharacter character, SkillType skillType, int? value = null) : base(character, skillType.ToString())
    {
        SkillType = skillType;
        Value = value ?? Default;
    }
    
    public SkillType SkillType
    {
        get => EnumHelper.Parse<SkillType>(Name);
        private init => Name = value.ToString();
    }
    
    public override string Path => nameof(Skill);
    
    public const int Default = 0;
}