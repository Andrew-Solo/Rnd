using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class Skills : IEnumerable<Skill>, IParametersProvider
{
    #region SkillList
    
    public Skills(ICharacter character)
    {
        Bruteforce = new Skill(character, SkillType.Bruteforce);
        Struggle = new Skill(character, SkillType.Struggle);
        Blocking = new Skill(character, SkillType.Blocking);
        Fortitude = new Skill(character, SkillType.Fortitude);
        Fencing = new Skill(character, SkillType.Fencing);
        Throwing = new Skill(character, SkillType.Throwing);
        Shooting = new Skill(character, SkillType.Shooting);
        Alchemy = new Skill(character, SkillType.Alchemy);
        Enchantment = new Skill(character, SkillType.Enchantment);
        Magic = new Skill(character, SkillType.Magic);
        Necromancy = new Skill(character, SkillType.Necromancy);
        Shamanism = new Skill(character, SkillType.Shamanism);
        Priesthood = new Skill(character, SkillType.Priesthood);
        Demonology = new Skill(character, SkillType.Demonology);
        Metamorphism = new Skill(character, SkillType.Metamorphism);
        SleightOfHand = new Skill(character, SkillType.SleightOfHand);
        Acrobatics = new Skill(character, SkillType.Acrobatics);
        Stealth = new Skill(character, SkillType.Stealth);
        Reaction = new Skill(character, SkillType.Reaction);
        Tracking = new Skill(character, SkillType.Tracking);
        Navigation = new Skill(character, SkillType.Navigation);
        Riding = new Skill(character, SkillType.Riding);
        Streets = new Skill(character, SkillType.Streets);
        Survival = new Skill(character, SkillType.Survival);
        Rhetoric = new Skill(character, SkillType.Rhetoric);
        Empathy = new Skill(character, SkillType.Empathy);
        Polemics = new Skill(character, SkillType.Polemics);
        Networking = new Skill(character, SkillType.Networking);
        Authority = new Skill(character, SkillType.Authority);
        SelfControl = new Skill(character, SkillType.SelfControl);
        Research = new Skill(character, SkillType.Research);
        Medicine = new Skill(character, SkillType.Medicine);
        Nature = new Skill(character, SkillType.Nature);
        History = new Skill(character, SkillType.History);
        Society = new Skill(character, SkillType.Society);
        Engineering = new Skill(character, SkillType.Engineering);
        Science = new Skill(character, SkillType.Science);
        Metallurgy = new Skill(character, SkillType.Metallurgy);
        Farming = new Skill(character, SkillType.Farming);
        Mining = new Skill(character, SkillType.Mining);
        Construction = new Skill(character, SkillType.Construction);
        Craft = new Skill(character, SkillType.Craft);
        Mechanisms = new Skill(character, SkillType.Mechanisms);
        Chemistry = new Skill(character, SkillType.Chemistry);
        Treasures = new Skill(character, SkillType.Treasures);
        Culture = new Skill(character, SkillType.Culture);
        Creation = new Skill(character, SkillType.Creation);
        Inspiration = new Skill(character, SkillType.Inspiration);
        Performance = new Skill(character, SkillType.Performance);
        Artistry = new Skill(character, SkillType.Artistry);
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