namespace Rnd.Api.Modules.Basic.Effects;

public interface IEffectsProvider
{
    public IEnumerable<IEffect> Effects { get; }
}