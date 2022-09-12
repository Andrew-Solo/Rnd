using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.SkillParameters;

public class FinalSkills : Skills
{
    public FinalSkills(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    public override Skill Bruteforce => Character.Effects
        .Aggregate(new FinalSkill(base.Bruteforce), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Struggle => Character.Effects
        .Aggregate(new FinalSkill(base.Struggle), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Blocking => Character.Effects
        .Aggregate(new FinalSkill(base.Blocking), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Fortitude => Character.Effects
        .Aggregate(new FinalSkill(base.Fortitude), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Fencing => Character.Effects
        .Aggregate(new FinalSkill(base.Fencing), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Throwing => Character.Effects
        .Aggregate(new FinalSkill(base.Throwing), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Shooting => Character.Effects
        .Aggregate(new FinalSkill(base.Shooting), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Alchemy => Character.Effects
        .Aggregate(new FinalSkill(base.Alchemy), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Enchantment => Character.Effects
        .Aggregate(new FinalSkill(base.Enchantment), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Magic => Character.Effects
        .Aggregate(new FinalSkill(base.Magic), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Necromancy => Character.Effects
        .Aggregate(new FinalSkill(base.Necromancy), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Shamanism => Character.Effects
        .Aggregate(new FinalSkill(base.Shamanism), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Priesthood => Character.Effects
        .Aggregate(new FinalSkill(base.Priesthood), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Demonology => Character.Effects
        .Aggregate(new FinalSkill(base.Demonology), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Metamorphism => Character.Effects
        .Aggregate(new FinalSkill(base.Metamorphism), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill SleightOfHand => Character.Effects
        .Aggregate(new FinalSkill(base.SleightOfHand), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Acrobatics => Character.Effects
        .Aggregate(new FinalSkill(base.Acrobatics), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Stealth => Character.Effects
        .Aggregate(new FinalSkill(base.Stealth), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Reaction => Character.Effects
        .Aggregate(new FinalSkill(base.Reaction), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Tracking => Character.Effects
        .Aggregate(new FinalSkill(base.Tracking), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Navigation => Character.Effects
        .Aggregate(new FinalSkill(base.Navigation), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Riding => Character.Effects
        .Aggregate(new FinalSkill(base.Riding), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Streets => Character.Effects
        .Aggregate(new FinalSkill(base.Streets), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Survival => Character.Effects
        .Aggregate(new FinalSkill(base.Survival), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Rhetoric => Character.Effects
        .Aggregate(new FinalSkill(base.Rhetoric), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Empathy => Character.Effects
        .Aggregate(new FinalSkill(base.Empathy), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Polemics => Character.Effects
        .Aggregate(new FinalSkill(base.Polemics), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Networking => Character.Effects
        .Aggregate(new FinalSkill(base.Networking), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Authority => Character.Effects
        .Aggregate(new FinalSkill(base.Authority), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill SelfControl => Character.Effects
        .Aggregate(new FinalSkill(base.SelfControl), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Research => Character.Effects
        .Aggregate(new FinalSkill(base.Research), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Medicine => Character.Effects
        .Aggregate(new FinalSkill(base.Medicine), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Nature => Character.Effects
        .Aggregate(new FinalSkill(base.Nature), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill History => Character.Effects
        .Aggregate(new FinalSkill(base.History), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Society => Character.Effects
        .Aggregate(new FinalSkill(base.Society), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Engineering => Character.Effects
        .Aggregate(new FinalSkill(base.Engineering), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Science => Character.Effects
        .Aggregate(new FinalSkill(base.Science), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Metallurgy => Character.Effects
        .Aggregate(new FinalSkill(base.Metallurgy), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Farming => Character.Effects
        .Aggregate(new FinalSkill(base.Farming), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Mining => Character.Effects
        .Aggregate(new FinalSkill(base.Mining), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Construction => Character.Effects
        .Aggregate(new FinalSkill(base.Construction), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Craft => Character.Effects
        .Aggregate(new FinalSkill(base.Craft), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Mechanisms => Character.Effects
        .Aggregate(new FinalSkill(base.Mechanisms), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Chemistry => Character.Effects
        .Aggregate(new FinalSkill(base.Chemistry), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Treasures => Character.Effects
        .Aggregate(new FinalSkill(base.Treasures), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Culture => Character.Effects
        .Aggregate(new FinalSkill(base.Culture), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Creation => Character.Effects
        .Aggregate(new FinalSkill(base.Creation), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Inspiration => Character.Effects
        .Aggregate(new FinalSkill(base.Inspiration), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Performance => Character.Effects
        .Aggregate(new FinalSkill(base.Performance), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Skill Artistry => Character.Effects
        .Aggregate(new FinalSkill(base.Artistry), (attribute, effect) => effect.ModifyParameter(attribute));
}