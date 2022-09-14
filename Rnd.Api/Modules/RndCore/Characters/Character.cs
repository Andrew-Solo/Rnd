using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Members;
using Rnd.Api.Modules.RndCore.Effects;
using Rnd.Api.Modules.RndCore.Fields;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;
using Rnd.Api.Modules.RndCore.Parameters.DomainParameters;
using Rnd.Api.Modules.RndCore.Parameters.SkillParameters;
using Rnd.Api.Modules.RndCore.Resources.StateResources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Character : Basic.Characters.Character
{
    public Character(IEntity entity) : base(entity)
    {
        Attributes = new Attributes(this);
        Domains = new Domains(this);
        Skills = new Skills(this);
        Leveling = new Leveling(this);
        States = new States(this, Attributes, Leveling);

        CustomEffects = new CustomEffects();

        Final = new Final(this);
        
        General = new General(this);
        Additional = new Additional(this);
        Backstory = new Backstory(this);
    }
    
    public Character(Member owner, string name) : base(owner, name)
    {
        Attributes = new Attributes(this);
        Domains = new Domains(this);
        Skills = new Skills(this);
        Leveling = new Leveling(this);
        States = new States(this, Attributes, Leveling);

        CustomEffects = new CustomEffects();

        Final = new Final(this);
        
        General = new General(this);
        Additional = new Additional(this);
        Backstory = new Backstory(this);
    }
    
    public override string Module => nameof(RndCore);
    
    public Leveling Leveling { get; }
    public Attributes Attributes { get; }
    public Domains Domains { get; }
    public Skills Skills { get; }
    public States States { get; }

    public CustomEffects CustomEffects { get; }

    public Final Final { get; }
    
    public General General { get; }
    public Additional Additional { get; }
    public Backstory Backstory { get; }
}