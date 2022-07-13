using RnDBot.Models.Settings;

namespace RnDBot.Models;

public static class Glossary
{
    public static readonly Dictionary<AttributeType, string> AttributeAbbreviations = new()
    {
        [AttributeType.Str] = "СИЛ",
        [AttributeType.End] = "ТЕЛ",
        [AttributeType.Dex] = "ЛОВ",
        [AttributeType.Per] = "ВОС",
        [AttributeType.Int] = "ИНТ",
        [AttributeType.Wis] = "МУД",
        [AttributeType.Cha] = "ХАР",
        [AttributeType.Det] = "РЕШ",
    };
    
    public static readonly Dictionary<AttributeType, string> AttributeNames = new()
    {
        [AttributeType.Str] = "Сила",
        [AttributeType.End] = "Телосложение",
        [AttributeType.Dex] = "Ловкость",
        [AttributeType.Per] = "Восприятие",
        [AttributeType.Int] = "Интеллект",
        [AttributeType.Wis] = "Мудрость",
        [AttributeType.Cha] = "Харизма",
        [AttributeType.Det] = "Решимость",
    };
    
    public static readonly Dictionary<ConditionType, string> ConditionNames = new()
    {
        [ConditionType.Body] = "Тело",
        [ConditionType.Will] = "Воля",
        [ConditionType.Armor] = "Броня",
        [ConditionType.Barrier] = "Барьер",
        [ConditionType.Ap] = "Очки способностей",
    };

    public static readonly Dictionary<AncorniaDomainType, string> AncorniaDomainNames = new()
    {
        [AncorniaDomainType.War] = "Война",
        [AncorniaDomainType.Mist] = "Чудо",
        [AncorniaDomainType.Way] = "Путь",
        [AncorniaDomainType.Word] = "Слово",
        [AncorniaDomainType.Lore] = "Знание",
        [AncorniaDomainType.Craft] = "Ремесло",
        [AncorniaDomainType.Art] = "Искусство",
    };
    
    public static readonly Dictionary<AncorniaSkillType, string> AncorniaSkillNames = new()
    {
        [AncorniaSkillType.Bruteforce] = "Грубая сила",
        [AncorniaSkillType.Struggle] = "Борьба",
        [AncorniaSkillType.Fortitude] = "Стойкость",
        [AncorniaSkillType.Fencing] = "Фехтование",
        [AncorniaSkillType.HandToHandCombat] = "Рукопашный бой",
        [AncorniaSkillType.Throwing] = "Метание",
        [AncorniaSkillType.Shooting] = "Стрельба",
        [AncorniaSkillType.Riding] = "Верховая езда",
        [AncorniaSkillType.Alchemy] = "Алхимия",
        [AncorniaSkillType.Pyrokinetics] = "Пирокинетика",
        [AncorniaSkillType.Geomancy] = "Геомантия",
        [AncorniaSkillType.Aeroturgy] = "Аэротургия",
        [AncorniaSkillType.Hydrosophistry] = "Гидрософистика",
        [AncorniaSkillType.Enchantment] = "Зачарование",
        [AncorniaSkillType.Priesthood] = "Жречество",
        [AncorniaSkillType.Necromancy] = "Некромантия",
        [AncorniaSkillType.Demonology] = "Демонология",
        [AncorniaSkillType.Metamorphism] = "Метаморфизм",
        [AncorniaSkillType.Climbing] = "Лазанье",
        [AncorniaSkillType.SleightOfHand] = "Ловкость рук",
        [AncorniaSkillType.Acrobatics] = "Акробатика",
        [AncorniaSkillType.Stealth] = "Скрытность",
        [AncorniaSkillType.Reaction] = "Реакция",
        [AncorniaSkillType.Tracking] = "Следопытство",
        [AncorniaSkillType.Navigation] = "Навигация",
        [AncorniaSkillType.Streets] = "Улицы",
        [AncorniaSkillType.Survival] = "Выживание",
        [AncorniaSkillType.Empathy] = "Эмпатия",
        [AncorniaSkillType.Rhetoric] = "Риторика",
        [AncorniaSkillType.Manipulation] = "Манипуляция",
        [AncorniaSkillType.Networking] = "Связи",
        [AncorniaSkillType.Authority] = "Авторитет",
        [AncorniaSkillType.SelfControl] = "Самообладание",
        [AncorniaSkillType.Research] = "Исследование",
        [AncorniaSkillType.Engineering] = "Инженерия",
        [AncorniaSkillType.Medicine] = "Медицина",
        [AncorniaSkillType.Nature] = "Природа",
        [AncorniaSkillType.History] = "История",
        [AncorniaSkillType.Society] = "Общество",
        [AncorniaSkillType.Blacksmith] = "Кузнечное дело",
        [AncorniaSkillType.Farming] = "Фермерство",
        [AncorniaSkillType.Mining] = "Шахтерское дело",
        [AncorniaSkillType.Construction] = "Строительство",
        [AncorniaSkillType.Leatherworking] = "Кожевничество",
        [AncorniaSkillType.Tailoring] = "Портняжное дело",
        [AncorniaSkillType.Jewelry] = "Ювелирное дело",
        [AncorniaSkillType.Music] = "Музыка",
        [AncorniaSkillType.Culture] = "Культура",
        [AncorniaSkillType.Creation] = "Творчество",
        [AncorniaSkillType.Inspiration] = "Вдохновение",
        [AncorniaSkillType.Performance] = "Выступление",
        [AncorniaSkillType.Artistry] = "Артистизм",
    };

    public static readonly Dictionary<AncorniaSkillType, AttributeType> AncorniaSkillCoreAttributes = new()
    {
        [AncorniaSkillType.Bruteforce] = AttributeType.Str,
        [AncorniaSkillType.Struggle] = AttributeType.Str,
        [AncorniaSkillType.Fortitude] = AttributeType.End,
        [AncorniaSkillType.Fencing] = AttributeType.Dex,
        [AncorniaSkillType.HandToHandCombat] = AttributeType.Dex,
        [AncorniaSkillType.Throwing] = AttributeType.Dex,
        [AncorniaSkillType.Shooting] = AttributeType.Per,
        [AncorniaSkillType.Riding] = AttributeType.Per,
        [AncorniaSkillType.Alchemy] = AttributeType.Per,
        [AncorniaSkillType.Pyrokinetics] = AttributeType.Int,
        [AncorniaSkillType.Geomancy] = AttributeType.Int,
        [AncorniaSkillType.Aeroturgy] = AttributeType.Int,
        [AncorniaSkillType.Hydrosophistry] = AttributeType.Int,
        [AncorniaSkillType.Enchantment] = AttributeType.Int,
        [AncorniaSkillType.Priesthood] = AttributeType.Wis,
        [AncorniaSkillType.Necromancy] = AttributeType.Wis,
        [AncorniaSkillType.Demonology] = AttributeType.Cha,
        [AncorniaSkillType.Metamorphism] = AttributeType.Det,
        [AncorniaSkillType.Climbing] = AttributeType.Str,
        [AncorniaSkillType.SleightOfHand] = AttributeType.Dex,
        [AncorniaSkillType.Acrobatics] = AttributeType.Dex,
        [AncorniaSkillType.Stealth] = AttributeType.Dex,
        [AncorniaSkillType.Reaction] = AttributeType.Per,
        [AncorniaSkillType.Tracking] = AttributeType.Per,
        [AncorniaSkillType.Navigation] = AttributeType.Per,
        [AncorniaSkillType.Streets] = AttributeType.Wis,
        [AncorniaSkillType.Survival] = AttributeType.Det,
        [AncorniaSkillType.Empathy] = AttributeType.Wis,
        [AncorniaSkillType.Rhetoric] = AttributeType.Cha,
        [AncorniaSkillType.Manipulation] = AttributeType.Cha,
        [AncorniaSkillType.Networking] = AttributeType.Cha,
        [AncorniaSkillType.Authority] = AttributeType.Det,
        [AncorniaSkillType.SelfControl] = AttributeType.Det,
        [AncorniaSkillType.Research] = AttributeType.Per,
        [AncorniaSkillType.Engineering] = AttributeType.Int,
        [AncorniaSkillType.Medicine] = AttributeType.Int,
        [AncorniaSkillType.Nature] = AttributeType.Int,
        [AncorniaSkillType.History] = AttributeType.Int,
        [AncorniaSkillType.Society] = AttributeType.Int,
        [AncorniaSkillType.Blacksmith] = AttributeType.Str,
        [AncorniaSkillType.Farming] = AttributeType.End,
        [AncorniaSkillType.Mining] = AttributeType.End,
        [AncorniaSkillType.Construction] = AttributeType.End,
        [AncorniaSkillType.Leatherworking] = AttributeType.Dex,
        [AncorniaSkillType.Tailoring] = AttributeType.Cha,
        [AncorniaSkillType.Jewelry] = AttributeType.Dex,
        [AncorniaSkillType.Music] = AttributeType.Per,
        [AncorniaSkillType.Culture] = AttributeType.Int,
        [AncorniaSkillType.Creation] = AttributeType.Wis,
        [AncorniaSkillType.Inspiration] = AttributeType.Wis,
        [AncorniaSkillType.Performance] = AttributeType.Cha,
        [AncorniaSkillType.Artistry] = AttributeType.Cha,
    };

    public static string GetDomainDictionaryValue<TDomain>(TDomain domain)
    {
        return domain switch
        {
            AncorniaDomainType ancornia => AncorniaDomainNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(domain), domain, null)
        };
    }
    
    public static string GetSkillDictionaryValue<TSkill>(TSkill skill)
    {
        return skill switch
        {
            AncorniaSkillType ancornia => AncorniaSkillNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };
    }
}