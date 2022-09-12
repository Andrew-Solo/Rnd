using Rnd.Api.Modules.Basic.Effects;
using Rnd.Api.Modules.Basic.Fields;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Effects;
using Rnd.Api.Modules.RndCore.Fields;
using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;
using Rnd.Api.Modules.RndCore.Parameters.DomainParameters;
using Rnd.Api.Modules.RndCore.Parameters.SkillParameters;
using Rnd.Api.Modules.RndCore.Resources;
using Rnd.Api.Modules.RndCore.Resources.StateResources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Character : Basic.Characters.Character
{
    public Character(Guid ownerId, string name) : base(ownerId, name)
    {
        Attributes = new Attributes();
        Domains = new Domains();
        Skills = new Skills();
        Leveling = new Leveling(this);
        States = new States(this);

        CustomEffects = new CustomEffects();

        Final = new Final(this);
        
        General = new General();
        Additional = new Additional();
        Backstory = new Backstory();
    }
    
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
    
    #region Providers
    
    public override List<IResource> Resources
    {
        get
        {
            var result = new List<IResource>();

            foreach (var provider in ResourcesProviders)
            {
                result.AddRange(provider.Resources);
            }

            return result;
        }
    }
    
    
    public IEnumerable<IResourcesProvider> ResourcesProviders => new IResourcesProvider[] {Leveling, States};

    public override List<IParameter> Parameters
    {
        get
        {
            var result = new List<IParameter>();

            foreach (var provider in ParametersProviders)
            {
                result.AddRange(provider.Parameters);
            }

            return result;
        }
    }
    
    public IEnumerable<IParametersProvider> ParametersProviders => new IParametersProvider[] {Leveling, Attributes, Domains, Skills};

    public override List<IField> Fields
    {
        get
        {
            var result = new List<IField>();

            foreach (var provider in FieldsProviders)
            {
                result.AddRange(provider.Fields);
            }

            return result;
        }
    }
    
    public IEnumerable<IFieldsProvider> FieldsProviders => new IFieldsProvider[] {General, Additional, Backstory};
    
    public override List<IEffect> Effects
    {
        get
        {
            var result = new List<IEffect>();

            foreach (var provider in EffectsProviders)
            {
                result.AddRange(provider.Effects);
            }

            return result;
        }
    }
    
    public IEnumerable<IEffectsProvider> EffectsProviders => new IEffectsProvider[] {CustomEffects};
    
    #endregion
}