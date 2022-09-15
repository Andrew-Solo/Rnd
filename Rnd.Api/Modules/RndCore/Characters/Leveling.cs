using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Parameters;
using Rnd.Api.Modules.RndCore.Resources;
using Rnd.Api.Modules.RndCore.Resources.StateResources;

namespace Rnd.Api.Modules.RndCore.Characters;

public class Leveling : IResourcesProvider, IParametersProvider
{
    public Leveling(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    public Drama Drama => Character.Resources.FirstOrDefault(r => r.Path == nameof(Leveling) && r.Name == nameof(Drama))
        as Drama ?? new(Character);

    public Level Level => Character.Parameters.FirstOrDefault(p => p.Path == nameof(Leveling) && p.Name == nameof(Level))
        as Level ?? new(Character, GetLevel());

    public Damage Damage => Character.Parameters.FirstOrDefault(p => p.Path == nameof(Leveling) && p.Name == nameof(Damage))
        as Damage ?? new(Character, GetDamage());

    public Power Power => Character.Resources.FirstOrDefault(r => r.Path == nameof(Leveling) && r.Name == nameof(Power))
        as Power ?? new(Character, GetPower(), GetMaxPower());

    public MaxAttribute MaxAttribute => Character.Parameters.FirstOrDefault(p => p.Path == nameof(Leveling) && p.Name == nameof(MaxAttribute))
        as MaxAttribute ?? new(Character, GetMaxAttribute());

    public MaxSkill MaxSkill => Character.Parameters.FirstOrDefault(p => p.Path == nameof(Leveling) && p.Name == nameof(MaxSkill))
        as MaxSkill ?? new(Character, GetMaxSkill());

    public int GetLevel() => Character.Attributes.Sum(a => a.Value);
    public int GetDamage() => new[] {1, Level.Value / 16 + 1}.Max();
    public int GetMaxPower() => (int) Math.Floor(Math.Pow(2, ((double) Level.Value + 80) / 16));
    public int GetPower() => Character.Skills.Sum(s => s.Value);
    public int GetMaxAttribute() => Level.Value / 8 + 5;
    public int GetMaxSkill() => (int) Power.Max / 8 + 6;
    public int GetMaxEnergy() => (int) Power.Max / 10 + 1;
    
    public void CreateItems()
    {
        var objects = new object[] { Drama, Level, Damage, Power, MaxAttribute, MaxSkill };
    }

    #region Providers

    IEnumerable<IResource> IResourcesProvider.Resources => new IResource[] {Drama, Power};
    IEnumerable<IParameter> IParametersProvider.Parameters => new IParameter[] {Level, Damage, MaxAttribute, MaxSkill};

    #endregion
}