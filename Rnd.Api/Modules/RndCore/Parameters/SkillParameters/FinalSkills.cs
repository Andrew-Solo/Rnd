using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class FinalSkills : Skills
{
    public FinalSkills(ICharacter character) : base(character) { }

    public override Skill Bruteforce => GetSkill(SkillType.Bruteforce, true);
    public override Skill Struggle => GetSkill(SkillType.Struggle, true);
    public override Skill Blocking => GetSkill(SkillType.Blocking, true);
    public override Skill Fortitude => GetSkill(SkillType.Fortitude, true);
    public override Skill Fencing => GetSkill(SkillType.Fencing, true);
    public override Skill Throwing => GetSkill(SkillType.Throwing, true);
    public override Skill Shooting => GetSkill(SkillType.Shooting, true);
    public override Skill Alchemy => GetSkill(SkillType.Alchemy, true);
    public override Skill Enchantment => GetSkill(SkillType.Enchantment, true);
    public override Skill Magic => GetSkill(SkillType.Magic, true);
    public override Skill Necromancy => GetSkill(SkillType.Necromancy, true);
    public override Skill Shamanism => GetSkill(SkillType.Shamanism, true);
    public override Skill Priesthood => GetSkill(SkillType.Priesthood, true);
    public override Skill Demonology => GetSkill(SkillType.Demonology, true);
    public override Skill Metamorphism => GetSkill(SkillType.Metamorphism, true);
    public override Skill SleightOfHand => GetSkill(SkillType.SleightOfHand, true);
    public override Skill Acrobatics => GetSkill(SkillType.Acrobatics, true);
    public override Skill Stealth => GetSkill(SkillType.Stealth, true);
    public override Skill Reaction => GetSkill(SkillType.Reaction, true);
    public override Skill Tracking => GetSkill(SkillType.Tracking, true);
    public override Skill Navigation => GetSkill(SkillType.Navigation, true);
    public override Skill Riding => GetSkill(SkillType.Riding, true);
    public override Skill Streets => GetSkill(SkillType.Streets, true);
    public override Skill Survival => GetSkill(SkillType.Survival, true);
    public override Skill Rhetoric => GetSkill(SkillType.Rhetoric, true);
    public override Skill Empathy => GetSkill(SkillType.Empathy, true);
    public override Skill Polemics => GetSkill(SkillType.Polemics, true);
    public override Skill Networking => GetSkill(SkillType.Networking, true);
    public override Skill Authority => GetSkill(SkillType.Authority, true);
    public override Skill SelfControl => GetSkill(SkillType.SelfControl, true);
    public override Skill Research => GetSkill(SkillType.Research, true);
    public override Skill Medicine => GetSkill(SkillType.Medicine, true);
    public override Skill Nature => GetSkill(SkillType.Nature, true);
    public override Skill History => GetSkill(SkillType.History, true);
    public override Skill Society => GetSkill(SkillType.Society, true);
    public override Skill Engineering => GetSkill(SkillType.Engineering, true);
    public override Skill Science => GetSkill(SkillType.Science, true);
    public override Skill Metallurgy => GetSkill(SkillType.Metallurgy, true);
    public override Skill Farming => GetSkill(SkillType.Farming, true);
    public override Skill Mining => GetSkill(SkillType.Mining, true);
    public override Skill Construction => GetSkill(SkillType.Construction, true);
    public override Skill Craft => GetSkill(SkillType.Craft, true);
    public override Skill Mechanisms => GetSkill(SkillType.Mechanisms, true);
    public override Skill Chemistry => GetSkill(SkillType.Chemistry, true);
    public override Skill Treasures => GetSkill(SkillType.Treasures, true);
    public override Skill Culture => GetSkill(SkillType.Culture, true);
    public override Skill Creation => GetSkill(SkillType.Creation, true);
    public override Skill Inspiration => GetSkill(SkillType.Inspiration, true);
    public override Skill Performance => GetSkill(SkillType.Performance, true);
    public override Skill Artistry => GetSkill(SkillType.Artistry, true);
}