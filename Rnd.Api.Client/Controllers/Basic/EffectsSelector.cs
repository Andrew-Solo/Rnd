namespace Rnd.Api.Client.Controllers.Basic;

public class EffectsSelector : Selector
{
    public EffectsSelector(HttpClient client, Uri path) : base(client, path) { }
    
    public ParameterEffects ParameterEffects => new(Client, Path, true);
    public ResourceEffects ResourceEffects => new(Client, Path, true);
}