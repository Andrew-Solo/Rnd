using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;
using Rnd.Api.Modules.RndCore.Parameters.DomainParameters;
using Rnd.Api.Modules.RndCore.Parameters.SkillParameters;
using Rnd.Api.Modules.RndCore.Resources;
using Rnd.Api.Modules.RndCore.Resources.StateResources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Final : IParametersProvider, IResourcesProvider
{
    public Final(Character character)
    {
        Character = character;
    }

    public Character Character { get; }
    public Attributes Attributes => new FinalAttributes(Character);
    public Domains Domains => new();
    public Skills Skills => new();
    public States States => new(Character);

    #region Providers

    public IEnumerable<IParameter> Parameters => Attributes.Parameters.Union(Domains.Parameters).Union(Skills.Parameters);
    public IEnumerable<IResource> Resources => States.Resources;

    #endregion
}