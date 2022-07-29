using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Models.Character;

//Создает и предзаполняет объекты персонажей в зависимости от сеттинга
public static class CharacterFactory
{
    public static AbstractCharacter AbstractCharacter(string name, int level = 0)
    {
        var character = new AbstractCharacter(name, level)
        {
            General =
            {
                Description = "Этого персонажа я создал только для того, чтобы сохранить в нем свой публичный токен.",
                Culture = { TValue = "Культурный челик"},
                Age = { TValue = "22" },
                Ideals = { Values = new List<string> { "Вера в какашки", "Сострадание какашкам" }},
                Vices = { Values = new List<string> { "Капрофилия", "Дермовый чел, в целом" }},
                Traits = { Values = new List<string> { "Люблю поесть", "Люблю поспать", "Ленивый", "Окорочек" }},
            }
        };

        return character;
    }
    
    public static AncorniaCharacter AncorniaCharacter(string name, int level = 0)
    {
        var domains = new List<Domain<AncorniaDomainType, AncorniaSkillType>>
        {
            new(AncorniaDomainType.War, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Bruteforce),
                CreateSkill(AncorniaSkillType.Struggle),
                CreateSkill(AncorniaSkillType.Fortitude),
                CreateSkill(AncorniaSkillType.Fencing),
                CreateSkill(AncorniaSkillType.HandToHandCombat),
                CreateSkill(AncorniaSkillType.Throwing),
                CreateSkill(AncorniaSkillType.Shooting),
                CreateSkill(AncorniaSkillType.Riding),
            }, 4),
            new(AncorniaDomainType.Mist, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Alchemy),
                CreateSkill(AncorniaSkillType.Pyrokinetics),
                CreateSkill(AncorniaSkillType.Geomancy),
                CreateSkill(AncorniaSkillType.Aeroturgy),
                CreateSkill(AncorniaSkillType.Hydrosophistry),
                CreateSkill(AncorniaSkillType.Enchantment),
                CreateSkill(AncorniaSkillType.Priesthood),
                CreateSkill(AncorniaSkillType.Necromancy),
                CreateSkill(AncorniaSkillType.Demonology),
                CreateSkill(AncorniaSkillType.Metamorphism),
            }, 4),
            new(AncorniaDomainType.Way, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Climbing),
                CreateSkill(AncorniaSkillType.SleightOfHand),
                CreateSkill(AncorniaSkillType.Acrobatics),
                CreateSkill(AncorniaSkillType.Stealth),
                CreateSkill(AncorniaSkillType.Reaction),
                CreateSkill(AncorniaSkillType.Tracking),
                CreateSkill(AncorniaSkillType.Navigation),
                CreateSkill(AncorniaSkillType.Streets),
                CreateSkill(AncorniaSkillType.Survival),
            }, 4),
            new(AncorniaDomainType.Word, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Empathy),
                CreateSkill(AncorniaSkillType.Rhetoric),
                CreateSkill(AncorniaSkillType.Manipulation),
                CreateSkill(AncorniaSkillType.Networking),
                CreateSkill(AncorniaSkillType.Authority),
                CreateSkill(AncorniaSkillType.SelfControl),
            }, 4),
            new(AncorniaDomainType.Lore, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Research),
                CreateSkill(AncorniaSkillType.Engineering),
                CreateSkill(AncorniaSkillType.Medicine),
                CreateSkill(AncorniaSkillType.Nature),
                CreateSkill(AncorniaSkillType.History),
                CreateSkill(AncorniaSkillType.Society),
            }, 4),
            new(AncorniaDomainType.Craft, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Blacksmith),
                CreateSkill(AncorniaSkillType.Farming),
                CreateSkill(AncorniaSkillType.Mining),
                CreateSkill(AncorniaSkillType.Construction),
                CreateSkill(AncorniaSkillType.Leatherworking),
                CreateSkill(AncorniaSkillType.Tailoring),
            }, 4),
            new(AncorniaDomainType.Art, new List<Skill<AncorniaSkillType>>
            {
                CreateSkill(AncorniaSkillType.Jewelry),
                CreateSkill(AncorniaSkillType.Music),
                CreateSkill(AncorniaSkillType.Culture),
                CreateSkill(AncorniaSkillType.Creation),
                CreateSkill(AncorniaSkillType.Inspiration),
                CreateSkill(AncorniaSkillType.Performance),
                CreateSkill(AncorniaSkillType.Artistry),
            }, 4),
        };

        var character = new AncorniaCharacter(AbstractCharacter(name, level), domains);

        return character;
    }

    private static Skill<TSkill> CreateSkill<TSkill>(TSkill type) 
        where TSkill : struct => 
        new(Glossary.GetSkillCoreAttribute(type), type, 0);
}