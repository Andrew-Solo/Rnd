using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class FinalStates : States
{
    public FinalStates(Character character) : base (character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    
}