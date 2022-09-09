namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

public interface IEffectProvider
{
    public IEnumerable<IEffect> GetEffects();
}