using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class FinalSkills : Skills
{
    public FinalSkills(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    
}