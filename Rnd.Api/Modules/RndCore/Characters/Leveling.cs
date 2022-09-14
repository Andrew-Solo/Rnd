using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Resources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Leveling : IResourcesProvider, IParametersProvider
{
    public Leveling(Character character, int drama = 0)
    {
        Character = character;
        Drama = new Drama(character, drama);
    }
    
    public Character Character { get; }

    public Drama Drama { get; }
    public Level Level => new(Character, GetLevel());
    public Damage Damage => new(Character, GetDamage());
    public Power Power => new(Character, GetPower(), GetMaxPower());
    public MaxAttribute MaxAttribute => new(Character, GetMaxAttribute());
    public MaxSkill MaxSkill => new(Character, GetMaxSkill());

    public int GetLevel() => Character.Attributes.Sum(a => a.Value);
    public int GetDamage() => new[] {1, Level.Value / 16 + 1}.Max();
    public int GetMaxPower() => (int) Math.Floor(Math.Pow(2, ((double) Level.Value + 80) / 16));
    public int GetPower() => Character.Skills.Sum(s => s.Value);
    public int GetMaxAttribute() => Level.Value / 8 + 5;
    public int GetMaxSkill() => (int) Power.Max / 8 + 6;
    public int GetMaxEnergy() => (int) Power.Max / 10 + 1;

    #region Providers

    IEnumerable<IResource> IResourcesProvider.Resources => new IResource[] {Drama, Power};
    IEnumerable<IParameter> IParametersProvider.Parameters => new IParameter[] {Level, Damage, MaxAttribute, MaxSkill};

    #endregion

}