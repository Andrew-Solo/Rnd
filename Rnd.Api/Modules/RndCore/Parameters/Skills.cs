using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

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
    
    public Skill Bruteforce { get; }
    public Skill Struggle { get; }
    public Skill Blocking { get; }
    public Skill Fortitude { get; }
    public Skill Fencing { get; }
    public Skill Throwing { get; }
    public Skill Shooting { get; }
    public Skill Alchemy { get; }
    public Skill Enchantment { get; }
    public Skill Magic { get; }
    public Skill Necromancy { get; }
    public Skill Shamanism { get; }
    public Skill Priesthood { get; }
    public Skill Demonology { get; }
    public Skill Metamorphism { get; }
    public Skill SleightOfHand { get; }
    public Skill Acrobatics { get; }
    public Skill Stealth { get; }
    public Skill Reaction { get; }
    public Skill Tracking { get; }
    public Skill Navigation { get; }
    public Skill Riding { get; }
    public Skill Streets { get; }
    public Skill Survival { get; }
    public Skill Rhetoric { get; }
    public Skill Empathy { get; }
    public Skill Polemics { get; }
    public Skill Networking { get; }
    public Skill Authority { get; }
    public Skill SelfControl { get; }
    public Skill Research { get; }
    public Skill Medicine { get; }
    public Skill Nature { get; }
    public Skill History { get; }
    public Skill Society { get; }
    public Skill Engineering { get; }
    public Skill Science { get; }
    public Skill Metallurgy { get; }
    public Skill Farming { get; }
    public Skill Mining { get; }
    public Skill Construction { get; }
    public Skill Craft { get; }
    public Skill Mechanisms { get; }
    public Skill Chemistry { get; }
    public Skill Treasures { get; }
    public Skill Culture { get; }
    public Skill Creation { get; }
    public Skill Inspiration { get; }
    public Skill Performance { get; }
    public Skill Artistry { get; }

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