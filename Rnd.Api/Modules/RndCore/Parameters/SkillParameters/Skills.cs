using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;
using Path = Rnd.Api.Helpers.Path;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class Skills : IEnumerable<Skill>, IParametersProvider
{
    #region SkillList
    
    public Skills(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public virtual Skill Bruteforce => GetSkill(SkillType.Bruteforce);
    public virtual Skill Struggle => GetSkill(SkillType.Struggle);
    public virtual Skill Blocking => GetSkill(SkillType.Blocking);
    public virtual Skill Fortitude => GetSkill(SkillType.Fortitude);
    public virtual Skill Fencing => GetSkill(SkillType.Fencing);
    public virtual Skill Throwing => GetSkill(SkillType.Throwing);
    public virtual Skill Shooting => GetSkill(SkillType.Shooting);
    public virtual Skill Alchemy => GetSkill(SkillType.Alchemy);
    public virtual Skill Enchantment => GetSkill(SkillType.Enchantment);
    public virtual Skill Magic => GetSkill(SkillType.Magic);
    public virtual Skill Necromancy => GetSkill(SkillType.Necromancy);
    public virtual Skill Shamanism => GetSkill(SkillType.Shamanism);
    public virtual Skill Priesthood => GetSkill(SkillType.Priesthood);
    public virtual Skill Demonology => GetSkill(SkillType.Demonology);
    public virtual Skill Metamorphism => GetSkill(SkillType.Metamorphism);
    public virtual Skill SleightOfHand => GetSkill(SkillType.SleightOfHand);
    public virtual Skill Acrobatics => GetSkill(SkillType.Acrobatics);
    public virtual Skill Stealth => GetSkill(SkillType.Stealth);
    public virtual Skill Reaction => GetSkill(SkillType.Reaction);
    public virtual Skill Tracking => GetSkill(SkillType.Tracking);
    public virtual Skill Navigation => GetSkill(SkillType.Navigation);
    public virtual Skill Riding => GetSkill(SkillType.Riding);
    public virtual Skill Streets => GetSkill(SkillType.Streets);
    public virtual Skill Survival => GetSkill(SkillType.Survival);
    public virtual Skill Rhetoric => GetSkill(SkillType.Rhetoric);
    public virtual Skill Empathy => GetSkill(SkillType.Empathy);
    public virtual Skill Polemics => GetSkill(SkillType.Polemics);
    public virtual Skill Networking => GetSkill(SkillType.Networking);
    public virtual Skill Authority => GetSkill(SkillType.Authority);
    public virtual Skill SelfControl => GetSkill(SkillType.SelfControl);
    public virtual Skill Research => GetSkill(SkillType.Research);
    public virtual Skill Medicine => GetSkill(SkillType.Medicine);
    public virtual Skill Nature => GetSkill(SkillType.Nature);
    public virtual Skill History => GetSkill(SkillType.History);
    public virtual Skill Society => GetSkill(SkillType.Society);
    public virtual Skill Engineering => GetSkill(SkillType.Engineering);
    public virtual Skill Science => GetSkill(SkillType.Science);
    public virtual Skill Metallurgy => GetSkill(SkillType.Metallurgy);
    public virtual Skill Farming => GetSkill(SkillType.Farming);
    public virtual Skill Mining => GetSkill(SkillType.Mining);
    public virtual Skill Construction => GetSkill(SkillType.Construction);
    public virtual Skill Craft => GetSkill(SkillType.Craft);
    public virtual Skill Mechanisms => GetSkill(SkillType.Mechanisms);
    public virtual Skill Chemistry => GetSkill(SkillType.Chemistry);
    public virtual Skill Treasures => GetSkill(SkillType.Treasures);
    public virtual Skill Culture => GetSkill(SkillType.Culture);
    public virtual Skill Creation => GetSkill(SkillType.Creation);
    public virtual Skill Inspiration => GetSkill(SkillType.Inspiration);
    public virtual Skill Performance => GetSkill(SkillType.Performance);
    public virtual Skill Artistry => GetSkill(SkillType.Artistry);
    
    protected Skill GetSkill(SkillType type, bool isFinal = false)
    {
        var path = Path.Combine(isFinal ? nameof(Final) : null, nameof(Skill));
        return Character.Parameters.FirstOrDefault(p => p.Path == path && p.Name == type.ToString()) as Skill 
               ?? (isFinal ? CreateFinal(type) : new Skill(Character, type));
    }
    
    private Skill CreateFinal(SkillType type)
    {
        return Character.Effects.Aggregate(
            new FinalSkill(Character, GetSkill(type)), 
            (skill, effect) => effect.ModifyParameter(skill));
    }

    #endregion
    
    public void CreateItems()
    {
        var objects = new object[]
        {
            Bruteforce, Struggle, Blocking, Fortitude, Fencing, Throwing, Shooting, Alchemy, Enchantment, Magic, Necromancy, Shamanism,
            Priesthood, Demonology, Metamorphism, SleightOfHand, Acrobatics, Stealth, Reaction, Tracking, Navigation, Riding, Streets,
            Survival, Rhetoric, Empathy, Polemics, Networking, Authority, SelfControl, Research, Medicine, Nature, History, Society,
            Engineering, Science, Metallurgy, Farming, Mining, Construction, Craft, Mechanisms, Chemistry, Treasures, Culture, Creation,
            Inspiration, Performance, Artistry
        };
    }
    
    #region IEnumerable

    public IEnumerator<Skill> GetEnumerator()
    {
        return new BasicEnumerator<Skill>(Bruteforce, Struggle, Blocking, Fortitude, Fencing, Throwing, Shooting, Alchemy, Enchantment,
            Magic, Necromancy, Shamanism, Priesthood, Demonology, Metamorphism, SleightOfHand, Acrobatics, Stealth, Reaction, Tracking,
            Navigation, Riding, Streets, Survival, Rhetoric, Empathy, Polemics, Networking, Authority, SelfControl, Research, Medicine,
            Nature, History, Society, Engineering, Science, Metallurgy, Farming, Mining, Construction, Craft, Mechanisms, Chemistry,
            Treasures, Culture, Creation, Inspiration, Performance, Artistry);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region IParametersProvider

    public IEnumerable<IParameter> Parameters => this;

    #endregion
}