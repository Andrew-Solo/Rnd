using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Skills : IEnumerable<Skill>, IParametersProvider
{
    public Skills()
    {
        Bruteforce = new Skill(SkillType.Bruteforce) {Value = Default};
        Struggle = new Skill(SkillType.Struggle) {Value = Default};
        Blocking = new Skill(SkillType.Blocking) {Value = Default};
        Fortitude = new Skill(SkillType.Fortitude) {Value = Default};
        Fencing = new Skill(SkillType.Fencing) {Value = Default};
        Throwing = new Skill(SkillType.Throwing) {Value = Default};
        Shooting = new Skill(SkillType.Shooting) {Value = Default};
        Alchemy = new Skill(SkillType.Alchemy) {Value = Default};
        Enchantment = new Skill(SkillType.Enchantment) {Value = Default};
        Magic = new Skill(SkillType.Magic) {Value = Default};
        Necromancy = new Skill(SkillType.Necromancy) {Value = Default};
        Shamanism = new Skill(SkillType.Shamanism) {Value = Default};
        Priesthood = new Skill(SkillType.Priesthood) {Value = Default};
        Demonology = new Skill(SkillType.Demonology) {Value = Default};
        Metamorphism = new Skill(SkillType.Metamorphism) {Value = Default};
        SleightOfHand = new Skill(SkillType.SleightOfHand) {Value = Default};
        Acrobatics = new Skill(SkillType.Acrobatics) {Value = Default};
        Stealth = new Skill(SkillType.Stealth) {Value = Default};
        Reaction = new Skill(SkillType.Reaction) {Value = Default};
        Tracking = new Skill(SkillType.Tracking) {Value = Default};
        Navigation = new Skill(SkillType.Navigation) {Value = Default};
        Riding = new Skill(SkillType.Riding) {Value = Default};
        Streets = new Skill(SkillType.Streets) {Value = Default};
        Survival = new Skill(SkillType.Survival) {Value = Default};
        Rhetoric = new Skill(SkillType.Rhetoric) {Value = Default};
        Empathy = new Skill(SkillType.Empathy) {Value = Default};
        Polemics = new Skill(SkillType.Polemics) {Value = Default};
        Networking = new Skill(SkillType.Networking) {Value = Default};
        Authority = new Skill(SkillType.Authority) {Value = Default};
        SelfControl = new Skill(SkillType.SelfControl) {Value = Default};
        Research = new Skill(SkillType.Research) {Value = Default};
        Medicine = new Skill(SkillType.Medicine) {Value = Default};
        Nature = new Skill(SkillType.Nature) {Value = Default};
        History = new Skill(SkillType.History) {Value = Default};
        Society = new Skill(SkillType.Society) {Value = Default};
        Engineering = new Skill(SkillType.Engineering) {Value = Default};
        Science = new Skill(SkillType.Science) {Value = Default};
        Metallurgy = new Skill(SkillType.Metallurgy) {Value = Default};
        Farming = new Skill(SkillType.Farming) {Value = Default};
        Mining = new Skill(SkillType.Mining) {Value = Default};
        Construction = new Skill(SkillType.Construction) {Value = Default};
        Craft = new Skill(SkillType.Craft) {Value = Default};
        Mechanisms = new Skill(SkillType.Mechanisms) {Value = Default};
        Chemistry = new Skill(SkillType.Chemistry) {Value = Default};
        Treasures = new Skill(SkillType.Treasures) {Value = Default};
        Culture = new Skill(SkillType.Culture) {Value = Default};
        Creation = new Skill(SkillType.Creation) {Value = Default};
        Inspiration = new Skill(SkillType.Inspiration) {Value = Default};
        Performance = new Skill(SkillType.Performance) {Value = Default};
        Artistry = new Skill(SkillType.Artistry) {Value = Default};
    }

    public const int Default = 0;

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