﻿using Rnd.Api.Helpers;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class FinalSkill : Skill
{
    public FinalSkill(Skill original) : base(original.SkillType, original.Value)
    {
        
    }

    public override string Path => PathHelper.Combine(nameof(Final), base.Path);
}