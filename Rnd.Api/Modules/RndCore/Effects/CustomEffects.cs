using Rnd.Api.Modules.Basic.Effects;

namespace Rnd.Api.Modules.RndCore.Effects;

public class CustomEffects : List<Custom>, IEffectsProvider
{
    #region IParametersProvider

    public IEnumerable<IEffect> Effects => this;

    #endregion
}