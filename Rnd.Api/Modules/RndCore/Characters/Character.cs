using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Resources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Character : Basic.Characters.Character
{
    public Character(Guid ownerId, string name) : base(ownerId, name)
    {
        Attributes = new Attributes();
        Domains = new Domains();
        Skills = new Skills();
        States = new States(this);
    }
    
    public Attributes Attributes { get; } 
    public Domains Domains { get; } 
    public Skills Skills { get; }
    public States States { get; } 
}