namespace RnDBot.Models.Character.Panels.Effect;

public interface IEffectProvider
{
    public IEnumerable<IEffect> GetEffects();
}