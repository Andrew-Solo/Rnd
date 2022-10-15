using Rnd.Api.Client.Models.Basic.Effect;

namespace Rnd.Api.Client.Controllers.Basic;

public class EffectsSelector : Selector<EffectModel, EffectFormModel>
{
    public EffectsSelector(HttpClient client, Uri path, IController<EffectModel, EffectFormModel> controller) 
        : base(client, path, controller) { }
    
    public ParameterEffects ParameterEffects => new(Client, Path, true);
    public ResourceEffects ResourceEffects => new(Client, Path, true);
}