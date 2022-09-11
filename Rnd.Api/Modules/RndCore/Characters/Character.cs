using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Resources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Character : Basic.Characters.Character, IResourcesProvider, IParametersProvider
{
    public Character(Guid ownerId, string name) : base(ownerId, name)
    {
        Attributes = new Attributes();
        Domains = new Domains();
        Skills = new Skills();
        States = new States(this);
    }

    public Drama Drama => new();
    public Level Level => new(0);
    public Damage Damage => new(1);
    public Power Power => new(0, 32);
    public Attributes Attributes { get; } 
    public Domains Domains { get; } 
    public Skills Skills { get; }
    public States States { get; }

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
    
    IEnumerable<IResource> IResourcesProvider.Resources => new IResource[] {Drama, Power};
    public IEnumerable<IResourcesProvider> ResourcesProviders => new IResourcesProvider[] {this, States};

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

    IEnumerable<IParameter> IParametersProvider.Parameters => new IParameter[] {Level, Damage};
    public IEnumerable<IParametersProvider> ParametersProviders => new IParametersProvider[] {this, Attributes, Domains, Skills};

    #endregion
}