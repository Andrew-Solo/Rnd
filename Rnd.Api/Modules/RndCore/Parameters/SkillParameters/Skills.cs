using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class Skills : IEnumerable<Skill>, IParametersProvider
{
    #region SkillList
    
    public Skills()
    {
        Bruteforce = new Skill(SkillType.Bruteforce);
        Struggle = new Skill(SkillType.Struggle);
        Blocking = new Skill(SkillType.Blocking);
        Fortitude = new Skill(SkillType.Fortitude);
        Fencing = new Skill(SkillType.Fencing);
        Throwing = new Skill(SkillType.Throwing);
        Shooting = new Skill(SkillType.Shooting);
        Alchemy = new Skill(SkillType.Alchemy);
        Enchantment = new Skill(SkillType.Enchantment);
        Magic = new Skill(SkillType.Magic);
        Necromancy = new Skill(SkillType.Necromancy);
        Shamanism = new Skill(SkillType.Shamanism);
        Priesthood = new Skill(SkillType.Priesthood);
        Demonology = new Skill(SkillType.Demonology);
        Metamorphism = new Skill(SkillType.Metamorphism);
        SleightOfHand = new Skill(SkillType.SleightOfHand);
        Acrobatics = new Skill(SkillType.Acrobatics);
        Stealth = new Skill(SkillType.Stealth);
        Reaction = new Skill(SkillType.Reaction);
        Tracking = new Skill(SkillType.Tracking);
        Navigation = new Skill(SkillType.Navigation);
        Riding = new Skill(SkillType.Riding);
        Streets = new Skill(SkillType.Streets);
        Survival = new Skill(SkillType.Survival);
        Rhetoric = new Skill(SkillType.Rhetoric);
        Empathy = new Skill(SkillType.Empathy);
        Polemics = new Skill(SkillType.Polemics);
        Networking = new Skill(SkillType.Networking);
        Authority = new Skill(SkillType.Authority);
        SelfControl = new Skill(SkillType.SelfControl);
        Research = new Skill(SkillType.Research);
        Medicine = new Skill(SkillType.Medicine);
        Nature = new Skill(SkillType.Nature);
        History = new Skill(SkillType.History);
        Society = new Skill(SkillType.Society);
        Engineering = new Skill(SkillType.Engineering);
        Science = new Skill(SkillType.Science);
        Metallurgy = new Skill(SkillType.Metallurgy);
        Farming = new Skill(SkillType.Farming);
        Mining = new Skill(SkillType.Mining);
        Construction = new Skill(SkillType.Construction);
        Craft = new Skill(SkillType.Craft);
        Mechanisms = new Skill(SkillType.Mechanisms);
        Chemistry = new Skill(SkillType.Chemistry);
        Treasures = new Skill(SkillType.Treasures);
        Culture = new Skill(SkillType.Culture);
        Creation = new Skill(SkillType.Creation);
        Inspiration = new Skill(SkillType.Inspiration);
        Performance = new Skill(SkillType.Performance);
        Artistry = new Skill(SkillType.Artistry);
    }
    
    public virtual Skill Bruteforce { get; }
    public virtual Skill Struggle { get; }
    public virtual Skill Blocking { get; }
    public virtual Skill Fortitude { get; }
    public virtual Skill Fencing { get; }
    public virtual Skill Throwing { get; }
    public virtual Skill Shooting { get; }
    public virtual Skill Alchemy { get; }
    public virtual Skill Enchantment { get; }
    public virtual Skill Magic { get; }
    public virtual Skill Necromancy { get; }
    public virtual Skill Shamanism { get; }
    public virtual Skill Priesthood { get; }
    public virtual Skill Demonology { get; }
    public virtual Skill Metamorphism { get; }
    public virtual Skill SleightOfHand { get; }
    public virtual Skill Acrobatics { get; }
    public virtual Skill Stealth { get; }
    public virtual Skill Reaction { get; }
    public virtual Skill Tracking { get; }
    public virtual Skill Navigation { get; }
    public virtual Skill Riding { get; }
    public virtual Skill Streets { get; }
    public virtual Skill Survival { get; }
    public virtual Skill Rhetoric { get; }
    public virtual Skill Empathy { get; }
    public virtual Skill Polemics { get; }
    public virtual Skill Networking { get; }
    public virtual Skill Authority { get; }
    public virtual Skill SelfControl { get; }
    public virtual Skill Research { get; }
    public virtual Skill Medicine { get; }
    public virtual Skill Nature { get; }
    public virtual Skill History { get; }
    public virtual Skill Society { get; }
    public virtual Skill Engineering { get; }
    public virtual Skill Science { get; }
    public virtual Skill Metallurgy { get; }
    public virtual Skill Farming { get; }
    public virtual Skill Mining { get; }
    public virtual Skill Construction { get; }
    public virtual Skill Craft { get; }
    public virtual Skill Mechanisms { get; }
    public virtual Skill Chemistry { get; }
    public virtual Skill Treasures { get; }
    public virtual Skill Culture { get; }
    public virtual Skill Creation { get; }
    public virtual Skill Inspiration { get; }
    public virtual Skill Performance { get; }
    public virtual Skill Artistry { get; }

    #endregion
    
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